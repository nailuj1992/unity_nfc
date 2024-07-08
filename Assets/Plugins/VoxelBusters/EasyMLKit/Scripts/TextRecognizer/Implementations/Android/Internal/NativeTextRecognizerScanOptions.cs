#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeTextRecognizerScanOptions : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeTextRecognizerScanOptions(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeTextRecognizerScanOptions(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeTextRecognizerScanOptions(string language) : base(Native.kClassName ,language)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeTextRecognizerScanOptions()
        {
            DebugLogger.Log("Disposing NativeTextRecognizerScanOptions");
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

        public string GetLanguage()
        {
            return Call<string>(Native.Method.kGetLanguage);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.textrecognitionv2.TextRecognizerScanOptions";

            internal class Method
            {
                internal const string kGetLanguage = "getLanguage";
            }

        }
    }
}
#endif