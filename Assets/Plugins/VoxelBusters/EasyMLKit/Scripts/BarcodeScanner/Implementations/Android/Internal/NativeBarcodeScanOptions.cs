#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeBarcodeScanOptions : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeBarcodeScanOptions(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeBarcodeScanOptions(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeBarcodeScanOptions() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeBarcodeScanOptions()
        {
            DebugLogger.Log("Disposing NativeBarcodeScanOptions");
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

        public int GetAllowedFormats()
        {
            return Call<int>(Native.Method.kGetAllowedFormats);
        }
        public void SetAllowedFormats(int allowedFormats)
        {
            Call(Native.Method.kSetAllowedFormats, allowedFormats);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.barcodescanner.googlemlkit.types.BarcodeScanOptions";

            internal class Method
            {
                internal const string kSetAllowedFormats = "setAllowedFormats";
                internal const string kGetAllowedFormats = "getAllowedFormats";
            }

        }
    }
}
#endif