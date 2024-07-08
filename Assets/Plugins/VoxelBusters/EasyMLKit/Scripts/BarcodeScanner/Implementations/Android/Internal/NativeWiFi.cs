#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeWiFi : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Public properties

        public const int TYPE_OPEN = 1;

        public const int TYPE_WPA = 2;

        public const int TYPE_WEP = 3;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeWiFi(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeWiFi(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeWiFi()
        {
            DebugLogger.Log("Disposing NativeWiFi");
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

        public int GetEncryptionType()
        {
            return Call<int>(Native.Method.kGetEncryptionType);
        }
        public string GetPassword()
        {
            return Call<string>(Native.Method.kGetPassword);
        }
        public string GetSsid()
        {
            return Call<string>(Native.Method.kGetSsid);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$WiFi";

            internal class Method
            {
                internal const string kGetPassword = "getPassword";
                internal const string kGetSsid = "getSsid";
                internal const string kGetEncryptionType = "getEncryptionType";
            }

        }
    }
}
#endif