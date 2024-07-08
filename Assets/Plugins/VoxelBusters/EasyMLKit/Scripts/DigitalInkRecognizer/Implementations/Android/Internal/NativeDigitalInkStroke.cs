#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkStroke : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkStroke(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkStroke(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeDigitalInkStroke() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkStroke()
        {
            DebugLogger.Log("Disposing NativeDigitalInkStroke");
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

        public void AddPoint(NativeDigitalInkPoint point)
        {
            Call(Native.Method.kAddPoint, point.NativeObject);
        }
        public NativeList<NativeDigitalInkPoint> GetPoints()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetPoints);
            if(nativeObj != null)
            {
                NativeList<NativeDigitalInkPoint> data  = new  NativeList<NativeDigitalInkPoint>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeDigitalInkPoint>);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkStroke";

            internal class Method
            {
                internal const string kAddPoint = "addPoint";
                internal const string kGetPoints = "getPoints";
            }

        }
    }
}
#endif