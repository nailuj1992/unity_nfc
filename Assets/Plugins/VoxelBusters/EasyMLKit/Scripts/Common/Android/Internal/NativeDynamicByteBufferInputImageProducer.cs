#if UNITY_ANDROID
using System;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeDynamicByteBufferInputImageProducer : NativeAbstractInputImageProducer
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDynamicByteBufferInputImageProducer() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeDynamicByteBufferInputImageProducer()
        {
            DebugLogger.Log("Disposing NativeDynamicByteBufferInputImageProducer");
        }
#endif
        #endregion
        #region Static methods
        private static AndroidJavaClass GetClass()
        {
            if (m_nativeClass == null)
            {
                m_nativeClass = new AndroidJavaClass(Native.kClassName);
            }
            return m_nativeClass;
        }

        #endregion
        #region Public methods

        public bool GetFetchNewCameraImage()
        {
            return Call<bool>(Native.Method.kGetFetchNewCameraImage);
        }

        public void SetFetchNewCameraImage(bool status)
        {
            Call(Native.Method.KSetFetchNewCameraImage, status);
        }

        public void SetNewByteBuffer(NativeByteBuffer buffer)
        {
            Call(Native.Method.kSetNewByteBuffer, buffer.NativeObject);
        }

        #endregion

        new internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.producersimpl.DynamicByteBufferInputImageProducer";

            internal class Method
            {
                internal const string kGetFetchNewCameraImage = "getFetchNewCameraImage";
                internal const string kSetNewByteBuffer = "setNewByteBuffer";
                internal const string KSetFetchNewCameraImage = "setFetchNewCameraImage";
            }
        }

        
    }
}
#endif