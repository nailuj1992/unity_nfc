#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkPoint : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkPoint(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkPoint(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeDigitalInkPoint(float x, float y, long timestamp) : base(Native.kClassName ,x, y, timestamp)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkPoint()
        {
            DebugLogger.Log("Disposing NativeDigitalInkPoint");
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

        public long GetTimestamp()
        {
            return Call<long>(Native.Method.kGetTimestamp);
        }
        public float GetX()
        {
            return Call<float>(Native.Method.kGetX);
        }
        public float GetY()
        {
            return Call<float>(Native.Method.kGetY);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkPoint";

            internal class Method
            {
                internal const string kGetTimestamp = "getTimestamp";
                internal const string kGetX = "getX";
                internal const string kGetY = "getY";
            }

        }
    }
}
#endif