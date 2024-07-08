#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;


namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeAbstractInputImageProducer : NativeAndroidJavaObjectWrapper
    {

#region Static properties

         private static AndroidJavaClass m_nativeClass;

#endregion

#region Constructor

        public NativeAbstractInputImageProducer() : base(Native.kClassName)
        {
        }

        public NativeAbstractInputImageProducer(string className, params object[] args) : base(className, args)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeAbstractInputImageProducer()
        {
            DebugLogger.Log("Disposing NativeAbstractInputImageProducer");
        }
#endif
#endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.producersimpl.AbstractInputImageProducer";

            internal class Method
            {
                //internal const string kSetImageAnalysisStrategy = "setImageAnalysisStrategy";
            }
        }
    }
}
#endif