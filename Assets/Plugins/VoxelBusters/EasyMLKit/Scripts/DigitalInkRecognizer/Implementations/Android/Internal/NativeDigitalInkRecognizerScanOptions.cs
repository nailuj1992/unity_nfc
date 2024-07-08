#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizerScanOptions : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkRecognizerScanOptions(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkRecognizerScanOptions(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeDigitalInkRecognizerScanOptions(NativeDigitalInkRecognizerModelIdentifier modelIdentifier, float inputWidth, float inputHeight, string preContext) : base(Native.kClassName ,modelIdentifier.NativeObject, inputWidth, inputHeight, preContext)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkRecognizerScanOptions()
        {
            DebugLogger.Log("Disposing NativeDigitalInkRecognizerScanOptions");
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

        public NativeDigitalInkRecognizerModelIdentifier GetModelIdentifier()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetModelIdentifier);
            if(nativeObj != null)
            {
                NativeDigitalInkRecognizerModelIdentifier data  = new  NativeDigitalInkRecognizerModelIdentifier(nativeObj);
                return data;
            }
            else
            {
                return default(NativeDigitalInkRecognizerModelIdentifier);
            }
        }
        public string GetPreContext()
        {
            return Call<string>(Native.Method.kGetPreContext);
        }
        public float GetWritingAreaHeight()
        {
            return Call<float>(Native.Method.kGetWritingAreaHeight);
        }
        public float GetWritingAreaWidth()
        {
            return Call<float>(Native.Method.kGetWritingAreaWidth);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkRecognizerScanOptions";

            internal class Method
            {
                internal const string kGetWritingAreaHeight = "getWritingAreaHeight";
                internal const string kGetPreContext = "getPreContext";
                internal const string kGetWritingAreaWidth = "getWritingAreaWidth";
                internal const string kGetModelIdentifier = "getModelIdentifier";
            }

        }
    }
}
#endif