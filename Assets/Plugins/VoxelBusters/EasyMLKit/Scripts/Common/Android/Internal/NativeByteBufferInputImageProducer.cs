#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeByteBufferInputImageProducer : NativeAbstractInputImageProducer
    {

#region Static properties

         private static AndroidJavaClass m_nativeClass;

#endregion

#region Constructor

        public NativeByteBufferInputImageProducer(NativeByteBuffer byteBuffer) : base(Native.kClassName, (object)byteBuffer.NativeObject)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeByteBufferInputImageProducer()
        {
            DebugLogger.Log("Disposing NativeByteBufferInputImageProducer");
        }
#endif
#endregion

        new internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.producersimpl.ByteBufferInputImageProducer";

            internal class Method
            {
                //internal const string kSetImageAnalysisStrategy = "setImageAnalysisStrategy";
            }
        }
    }
}
#endif