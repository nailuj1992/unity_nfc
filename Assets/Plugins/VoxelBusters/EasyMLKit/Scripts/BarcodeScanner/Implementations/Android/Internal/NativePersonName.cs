#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativePersonName : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativePersonName(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativePersonName(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativePersonName()
        {
            DebugLogger.Log("Disposing NativePersonName");
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

        public string GetFirst()
        {
            return Call<string>(Native.Method.kGetFirst);
        }
        public string GetFormattedName()
        {
            return Call<string>(Native.Method.kGetFormattedName);
        }
        public string GetLast()
        {
            return Call<string>(Native.Method.kGetLast);
        }
        public string GetMiddle()
        {
            return Call<string>(Native.Method.kGetMiddle);
        }
        public string GetPrefix()
        {
            return Call<string>(Native.Method.kGetPrefix);
        }
        public string GetPronunciation()
        {
            return Call<string>(Native.Method.kGetPronunciation);
        }
        public string GetSuffix()
        {
            return Call<string>(Native.Method.kGetSuffix);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$PersonName";

            internal class Method
            {
                internal const string kGetFirst = "getFirst";
                internal const string kGetLast = "getLast";
                internal const string kGetMiddle = "getMiddle";
                internal const string kGetSuffix = "getSuffix";
                internal const string kGetPrefix = "getPrefix";
                internal const string kGetPronunciation = "getPronunciation";
                internal const string kGetFormattedName = "getFormattedName";
            }

        }
    }
}
#endif