#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizerModelIdentifier : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkRecognizerModelIdentifier(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkRecognizerModelIdentifier(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeDigitalInkRecognizerModelIdentifier(string identifier) : base(Native.kClassName ,identifier)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkRecognizerModelIdentifier()
        {
            DebugLogger.Log("Disposing NativeDigitalInkRecognizerModelIdentifier");
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

        public string GetIdentifier()
        {
            return Call<string>(Native.Method.kGetIdentifier);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkRecognizerModelIdentifier";

            internal class Method
            {
                internal const string kGetIdentifier = "getIdentifier";
            }

        }
    }
}
#endif