#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;


namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeLiveCameraInputImageProducer : NativeAbstractInputImageProducer
    {

#region Static properties

         private static AndroidJavaClass m_nativeClass;

#endregion

#region Constructor

        public NativeLiveCameraInputImageProducer(NativeContext context, NativeViewGroup previewContainer, NativeLiveCameraOptions options) : base(Native.kClassName, (object)context.NativeObject, (object)previewContainer.NativeObject, (object)options.NativeObject)
        {
        }

#if NATIVE_PLUGINS_DEBUG_ENABLED
        ~NativeLiveCameraInputImageProducer()
        {
            DebugLogger.Log("Disposing NativeLiveCameraInputImageProducer");
        }
#endif
#endregion

        new internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.producersimpl.LiveCameraInputImageProducer";

            internal class Method
            {
                //internal const string kSetImageAnalysisStrategy = "setImageAnalysisStrategy";
            }
        }
    }
}
#endif