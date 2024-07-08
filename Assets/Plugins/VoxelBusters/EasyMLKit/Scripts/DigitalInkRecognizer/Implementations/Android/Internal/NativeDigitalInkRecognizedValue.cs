#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizedValue : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkRecognizedValue(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkRecognizedValue(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkRecognizedValue()
        {
            DebugLogger.Log("Disposing NativeDigitalInkRecognizedValue");
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

        public float GetScore()
        {
            return Call<float>(Native.Method.kGetScore);
        }
        public string GetText()
        {
            return Call<string>(Native.Method.kGetText);
        }
        public bool HasScore()
        {
            return Call<bool>(Native.Method.kHasScore);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkRecognizedValue";

            internal class Method
            {
                internal const string kGetScore = "getScore";
                internal const string kHasScore = "hasScore";
                internal const string kGetText = "getText";
            }

        }
    }
}
#endif