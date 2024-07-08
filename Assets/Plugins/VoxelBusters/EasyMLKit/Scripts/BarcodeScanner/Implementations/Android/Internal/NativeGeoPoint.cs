#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeGeoPoint : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeGeoPoint(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeGeoPoint(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeGeoPoint()
        {
            DebugLogger.Log("Disposing NativeGeoPoint");
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

        public double GetLat()
        {
            return Call<double>(Native.Method.kGetLat);
        }
        public double GetLng()
        {
            return Call<double>(Native.Method.kGetLng);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$GeoPoint";

            internal class Method
            {
                internal const string kGetLng = "getLng";
                internal const string kGetLat = "getLat";
            }

        }
    }
}
#endif