#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeAddress : NativeAndroidJavaObjectWrapper
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
        public NativeAddress(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeAddress(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeAddress()
        {
            DebugLogger.Log("Disposing NativeAddress");
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

        public string[] GetAddressLines()
        {
            return Call<string[]>(Native.Method.kGetAddressLines);
        }
        public new int GetType()
        {
            return Call<int>(Native.Method.kGetType);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$Address";

            internal class Method
            {
                internal const string kGetType = "getType";
                internal const string kGetAddressLines = "getAddressLines";
            }

        }
    }
}
#endif