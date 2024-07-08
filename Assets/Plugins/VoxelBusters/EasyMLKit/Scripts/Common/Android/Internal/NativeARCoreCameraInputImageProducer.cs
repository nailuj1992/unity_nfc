#if UNITY_ANDROID && EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using System;
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeARCoreCameraInputImageProducer : NativeAbstractInputImageProducer
    {

#region Static properties

         private static AndroidJavaClass m_nativeClass;

#endregion

#region Constructor

        public NativeARCoreCameraInputImageProducer(long sessionHandle) : base(Native.kClassName, (object)sessionHandle)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeARCoreCameraInputImageProducer()
        {
            DebugLogger.Log("Disposing NativeARCoreCameraInputImageProducer");
        }
#endif
#endregion

#region Methods

        public bool GetFetchNewCameraImage()
        {
            return Call<bool>(Native.Method.KGetFetchNewCameraImage);
        }

        public void SetFetchNewCameraImage(bool status)
        {
            Call(Native.Method.KSetFetchNewCameraImage, status);
        }

        public void SetLatestCameraImage(NativeByteBuffer buffer, int width, int height, int rotation)
        {
#if NATIVE_PLUGINS_DEBUG_ENABLED
            DebugLogger.Log("[Class : NativeARCoreCameraInputImageProducer][Method : SetLatestCameraImage]");
#endif
            Call(Native.Method.kSetLatestFrame, new object[] { buffer.NativeObject, width, height, rotation });
        }
#endregion

        new internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.producersimpl.ARCoreCameraInputImageProducer";

            internal class Method
            {
                internal const string kSetLatestFrame = "setLatestCameraImage";
                internal const string KGetFetchNewCameraImage = "getFetchNewCameraImage";
                internal const string KSetFetchNewCameraImage = "setFetchNewCameraImage";

            }
        }
    }
}
#endif