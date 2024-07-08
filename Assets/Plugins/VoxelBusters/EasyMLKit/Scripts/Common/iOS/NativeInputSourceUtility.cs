#if UNITY_IOS
using System;
using UnityEngine;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    public static class NativeInputSourceUtility
    {
        public static IntPtr CreateInputSource(IInputSource inputSource)
        {
            if (inputSource is LiveCameraInputSource)
            {
                throw new Exception("Live camera input source not yet implemented on iOS");
            }
            else if (inputSource is ImageInputSource)
            {
                ImageInputSource imageInput = inputSource as ImageInputSource;
                NativeImageInputSource nativeInputSource = new NativeImageInputSource(imageInput.GetBytes());
                return nativeInputSource.NativeHandle;
            }
            else if (inputSource is WebCamTextureInputSource)
            {
                throw new Exception("WebCamTexture input source is not yet implemented on iOS");
            }
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
            else if (inputSource is ARFoundationCameraInputSource)
            {
                ARFoundationCameraInputSource arInputSource = (ARFoundationCameraInputSource)inputSource;
                NativeArKitInputSource nativeInputSource = new NativeArKitInputSource(arInputSource.Session);

                arInputSource.Register();
                arInputSource.CameraFrameReceived += () =>
                {
                    nativeInputSource.OnCameraFrameReceived();
                };

                return nativeInputSource.NativeHandle;
            }
#endif
            else
            {
                throw new Exception("Input source not implemented!");
            }
        }
    }
}
#endif