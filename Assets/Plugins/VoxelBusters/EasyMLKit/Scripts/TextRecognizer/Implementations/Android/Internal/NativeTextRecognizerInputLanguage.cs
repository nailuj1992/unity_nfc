#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeTextRecognizerInputLanguage : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Public properties

        public const string Latin = "latin";

        public const string Chinese = "chinese";

        public const string Devanagari = "devanagari";

        public const string Japanese = "japanese";

        public const string Korean = "korean";

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeTextRecognizerInputLanguage(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeTextRecognizerInputLanguage(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeTextRecognizerInputLanguage() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeTextRecognizerInputLanguage()
        {
            DebugLogger.Log("Disposing NativeTextRecognizerInputLanguage");
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
        public static bool IsValidInputLanguage(string language)
        {
            return GetClass().CallStatic<bool>(Native.Method.kIsValidInputLanguage, language);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.textrecognitionv2.TextRecognizerInputLanguage";

            internal class Method
            {
                internal const string kIsValidInputLanguage = "isValidInputLanguage";
            }

        }
    }
}
#endif