#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeGoogleMLKitTextRecognizer : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

       public NativeGoogleMLKitTextRecognizer(NativeContext context) : base(Native.kClassName ,(object)context.NativeObject)
       {
       }

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

        public void Close()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeGoogleMLKitTextRecognizer][Method : Close]");
#endif
            Call(Native.Method.kClose);
        }
        public string GetFeatureName()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeGoogleMLKitTextRecognizer][Method : GetFeatureName]");
#endif
            return Call<string>(Native.Method.kGetFeatureName);
        }
        public void Prepare(NativeAbstractInputImageProducer imageProducer, NativeTextRecognizerScanOptions options)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeGoogleMLKitTextRecognizer][Method : Prepare]");
#endif
            Call(Native.Method.kPrepare, new object[] { imageProducer.NativeObject, options.NativeObject } );
        }
        public void Process()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeGoogleMLKitTextRecognizer][Method : Process]");
#endif
            Call(Native.Method.kProcess);
        }
        public void SetListener(NativeGoogleMLKitTextRecognizerListener listener)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeGoogleMLKitTextRecognizer][Method : SetListener]");
#endif
            Call(Native.Method.kSetListener, new object[] { listener } );
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.textrecognitionv2.googlemlkit.GoogleMLKitTextRecognizer";

            internal class Method
            {
                internal const string kPrepare = "prepare";
                internal const string kSetListener = "setListener";
                internal const string kProcess = "process";
                internal const string kGetFeatureName = "getFeatureName";
                internal const string kClose = "close";
            }

        }
    }
}
#endif