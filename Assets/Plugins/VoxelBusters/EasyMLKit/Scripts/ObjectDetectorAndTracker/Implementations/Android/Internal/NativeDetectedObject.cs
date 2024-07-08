#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDetectedObject : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDetectedObject(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDetectedObject(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeDetectedObject()
        {
            DebugLogger.Log("Disposing NativeDetectedObject");
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

        public bool Equals(NativeObject arg0)
        {
            return Call<bool>(Native.Method.kEquals, arg0.NativeObject);
        }
        public NativeRect GetBoundingBox()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetBoundingBox);
            if(nativeObj != null)
            {
                NativeRect data  = new  NativeRect(nativeObj);
                return data;
            }
            else
            {
                return default(NativeRect);
            }
        }
        public NativeList<NativeLabel> GetLabels()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetLabels);
            if(nativeObj != null)
            {
                NativeList<NativeLabel> data  = new  NativeList<NativeLabel>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeLabel>);
            }
        }
        public NativeInteger GetTrackingId()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetTrackingId);
            if(nativeObj != null)
            {
                NativeInteger data  = new  NativeInteger(nativeObj);
                return data;
            }
            else
            {
                return default(NativeInteger);
            }
        }
        public int HashCode()
        {
            return Call<int>(Native.Method.kHashCode);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.objects.DetectedObject";

            internal class Method
            {
                internal const string kHashCode = "hashCode";
                internal const string kGetTrackingId = "getTrackingId";
                internal const string kGetLabels = "getLabels";
                internal const string kGetBoundingBox = "getBoundingBox";
                internal const string kEquals = "equals";
            }

        }
    }
}
#endif