#if UNITY_ANDROID
using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeImageBasedInputFeatureBase
    {
        protected IImageInputSource m_inputSource;

        private NativeSlice<sbyte> m_nativeSlice;
        private sbyte[] m_nativeSbytes;
        private Texture2D m_texture2d;

        protected NativeImageBasedInputFeatureBase()
        {
        }

        protected void Prepare(IImageInputSource inputSource)
        {
            m_inputSource = inputSource;
        }


        protected NativeAbstractInputImageProducer GetNativeInputImageProducer(IImageInputSource inputSource)
        {
            if (inputSource is LiveCameraInputSource)
            {
                NativeLiveCameraOptions liveCameraOptions = new NativeLiveCameraOptions();
                liveCameraOptions.SetFlashEnabled(((LiveCameraInputSource)inputSource).EnableFlash);
                liveCameraOptions.SetUseFrontCam(((LiveCameraInputSource)inputSource).IsFrontFacing);

                NativeLiveCameraInputImageProducer nativeInputProducer = new NativeLiveCameraInputImageProducer(NativeUnityPluginUtility.GetContext(), NativeUnityPluginUtility.GetDecorRootView(), liveCameraOptions);
                return nativeInputProducer;
            }
            else if (inputSource is ImageInputSource)
            {
                NativeByteBufferInputImageProducer nativeInputProducer = new NativeByteBufferInputImageProducer(NativeByteBuffer.Wrap(((ImageInputSource)inputSource).GetBytes()));
                return nativeInputProducer;
            }
            else if (inputSource is WebCamTextureInputSource)
            {
                NativeDynamicByteBufferInputImageProducer nativeInputProducer = new NativeDynamicByteBufferInputImageProducer();
                WebCamTextureInputSource webCamTextureInput = inputSource as WebCamTextureInputSource;

                webCamTextureInput.CameraFrameReceived += () =>
                {
                    if (!nativeInputProducer.GetFetchNewCameraImage())
                        return;

                    nativeInputProducer.SetFetchNewCameraImage(false); //Set this to make sure we don't accept one more while processing current one

#if UNITY_2020_3_OR_NEWER
                    if (SystemInfo.supportsAsyncGPUReadback)
                    {
                        if (m_nativeSlice == null)
                        {
                            NativeArray<byte> array = new NativeArray<byte>(webCamTextureInput.Texture.width * webCamTextureInput.Texture.height * 4, Allocator.Persistent,
                                            NativeArrayOptions.UninitializedMemory);

                            m_nativeSlice = new NativeSlice<byte>(array).SliceConvert<sbyte>();
                            m_nativeSbytes = new sbyte[m_nativeSlice.Length];
                        }

                        AsyncGPUReadback.RequestIntoNativeSlice(ref m_nativeSlice, webCamTextureInput.Texture, 0, (req) =>
                        {
                            if (req.hasError)
                            {
                                Debug.LogError("AsyncGPUReadback error!");
                                return;
                            }

                            nativeInputProducer.SetNewByteBuffer(GetNativeByteBuffer(m_nativeSlice, m_nativeSbytes));
                        });
                    }
                    else
#endif
                    {
                        if(m_texture2d == null)
                        {
                            m_texture2d = new Texture2D(webCamTextureInput.Texture.width, webCamTextureInput.Texture.height, TextureFormat.RGBA32, false);
                        }

                        m_texture2d.SetPixels(webCamTextureInput.Texture.GetPixels());
                        m_texture2d.Apply();

                        var bytes = m_texture2d.EncodeToJPG();
                        nativeInputProducer.SetNewByteBuffer(NativeByteBuffer.Wrap(bytes));
                    }
                };

                return nativeInputProducer;
            }
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
            else if (inputSource is ARFoundationCameraInputSource)
            {
                ARFoundationCameraInputSource arInputSource = (ARFoundationCameraInputSource)inputSource;
                NativeARCoreCameraInputImageProducer nativeInputProducer = new NativeARCoreCameraInputImageProducer(((ARFoundationCameraInputSource)inputSource).Session.subsystem.nativePtr.ToInt64());
                arInputSource.Register();
                arInputSource.CameraFrameReceived += () =>
                {
                    //Only fetch if past processing is done!
                    if (!nativeInputProducer.GetFetchNewCameraImage())
                        return;


                    UnityEngine.XR.ARSubsystems.XRCpuImage cpuImage = arInputSource.GetLatestCpuImage();

                    if (cpuImage != null && cpuImage.width > 0)
                    {
                        nativeInputProducer.SetFetchNewCameraImage(false); //Set this to make sure we don't accept one more while processing current one
                        //Get each plane and convert to byte buffer
                        NativeArCoreCameraImageBuilder builder = new NativeArCoreCameraImageBuilder();
                        builder.WithWidth(cpuImage.width);
                        builder.WithHeight(cpuImage.height);
                        builder.WithNumberOfPlanes(cpuImage.planeCount);
                        builder.WithTimestamp((long)cpuImage.timestamp);

                        for (int planeIndex = 0; planeIndex < cpuImage.planeCount; planeIndex++)
                        {
                            UnityEngine.XR.ARSubsystems.XRCpuImage.Plane each = cpuImage.GetPlane(planeIndex);
                            builder.AddPlane(planeIndex, each.rowStride, each.pixelStride, GetNativeByteBuffer(each.data));
                        }

                        NativeArCoreCameraImage image = builder.Build();
                        nativeInputProducer.SetLatestCameraImage(image.ToNV21Format(), image.GetWidth(), image.GetHeight(), (int)GetScreenRotation());
                    }
                    
                    //For disposing later we need to do it on native for ease (Native C)
                    cpuImage.Dispose();
                };

                return nativeInputProducer;
            }
#endif
            else
            {
                throw new Exception("Input source not implemented!");
            }
        }

        protected void Close()
        {
            m_inputSource.Close();
        }

        private NativeByteBuffer GetNativeByteBuffer(NativeArray<byte> array)
        {
            var slice = new NativeSlice<byte>(array).SliceConvert<sbyte>();
            var data = new sbyte[slice.Length];
            return GetNativeByteBuffer(slice, new sbyte[slice.Length]);
        }

        private NativeByteBuffer GetNativeByteBuffer(NativeSlice<sbyte> slice, sbyte[] target)
        {
            slice.CopyTo(target);
            return NativeByteBuffer.Wrap(target);
        }

        private float GetScreenRotation()
        {
            switch (Screen.orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                    return 0f;
                case ScreenOrientation.Portrait:
                    return 90f;
                case ScreenOrientation.LandscapeRight:
                    return 0;
                case ScreenOrientation.PortraitUpsideDown:
                    return 270f;
                default: return 0f;
            }
        }
    }
}
#endif
