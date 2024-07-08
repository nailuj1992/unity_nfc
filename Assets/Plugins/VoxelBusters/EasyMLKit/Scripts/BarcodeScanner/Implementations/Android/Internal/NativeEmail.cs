#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeEmail : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Public properties

        public const int TYPE_UNKNOWN = 0;

        public const int TYPE_WORK = 1;

        public const int TYPE_HOME = 2;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeEmail(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeEmail(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeEmail()
        {
            DebugLogger.Log("Disposing NativeEmail");
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

        public string GetAddress()
        {
            return Call<string>(Native.Method.kGetAddress);
        }
        public string GetBody()
        {
            return Call<string>(Native.Method.kGetBody);
        }
        public string GetSubject()
        {
            return Call<string>(Native.Method.kGetSubject);
        }
        public new int GetType()
        {
            return Call<int>(Native.Method.kGetType);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$Email";

            internal class Method
            {
                internal const string kGetBody = "getBody";
                internal const string kGetType = "getType";
                internal const string kGetAddress = "getAddress";
                internal const string kGetSubject = "getSubject";
            }

        }
    }
}
#endif