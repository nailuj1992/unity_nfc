#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizer : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

       public NativeDigitalInkRecognizer(NativeContext context) : base(Native.kClassName ,(object)context.NativeObject)
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
            DebugLogger.Log("[Class : NativeDigitalInkRecognizer][Method : Close]");
#endif
            Call(Native.Method.kClose);
        }
        public string GetFeatureName()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeDigitalInkRecognizer][Method : GetFeatureName]");
#endif
            return Call<string>(Native.Method.kGetFeatureName);
        }
        public NativeDigitalInkRecognizerModelManager GetModelManager()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeDigitalInkRecognizer][Method : GetModelManager]");
#endif
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetModelManager);
            NativeDigitalInkRecognizerModelManager data  = new  NativeDigitalInkRecognizerModelManager(nativeObj);
            return data;
        }
        public void Prepare(NativeDigitalInkRecognizerInput input, NativeDigitalInkRecognizerScanOptions scanOptions)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeDigitalInkRecognizer][Method : Prepare]");
#endif
            Call(Native.Method.kPrepare, new object[] { input.NativeObject, scanOptions.NativeObject } );
        }
        public void Process()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeDigitalInkRecognizer][Method : Process]");
#endif
            Call(Native.Method.kProcess);
        }
        public void SetListener(NativeDigitalInkRecognizerListener listener)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Class : NativeDigitalInkRecognizer][Method : SetListener]");
#endif
            Call(Native.Method.kSetListener, new object[] { listener } );
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.googlemlkit.DigitalInkRecognizer";

            internal class Method
            {
                internal const string kPrepare = "prepare";
                internal const string kSetListener = "setListener";
                internal const string kProcess = "process";
                internal const string kGetFeatureName = "getFeatureName";
                internal const string kGetModelManager = "getModelManager";
                internal const string kClose = "close";
            }

        }
    }
}
#endif