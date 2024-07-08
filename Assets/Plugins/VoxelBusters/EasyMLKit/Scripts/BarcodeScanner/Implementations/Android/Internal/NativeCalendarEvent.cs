#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeCalendarEvent : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeCalendarEvent(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeCalendarEvent(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeCalendarEvent()
        {
            DebugLogger.Log("Disposing NativeCalendarEvent");
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

        public string GetDescription()
        {
            return Call<string>(Native.Method.kGetDescription);
        }
        public NativeCalendarDateTime GetEnd()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetEnd);
            if(nativeObj != null)
            {
                NativeCalendarDateTime data  = new  NativeCalendarDateTime(nativeObj);
                return data;
            }
            else
            {
                return default(NativeCalendarDateTime);
            }
        }
        public string GetLocation()
        {
            return Call<string>(Native.Method.kGetLocation);
        }
        public string GetOrganizer()
        {
            return Call<string>(Native.Method.kGetOrganizer);
        }
        public NativeCalendarDateTime GetStart()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetStart);
            if(nativeObj != null)
            {
                NativeCalendarDateTime data  = new  NativeCalendarDateTime(nativeObj);
                return data;
            }
            else
            {
                return default(NativeCalendarDateTime);
            }
        }
        public string GetStatus()
        {
            return Call<string>(Native.Method.kGetStatus);
        }
        public string GetSummary()
        {
            return Call<string>(Native.Method.kGetSummary);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$CalendarEvent";

            internal class Method
            {
                internal const string kGetStart = "getStart";
                internal const string kGetOrganizer = "getOrganizer";
                internal const string kGetLocation = "getLocation";
                internal const string kGetSummary = "getSummary";
                internal const string kGetStatus = "getStatus";
                internal const string kGetDescription = "getDescription";
                internal const string kGetEnd = "getEnd";
            }

        }
    }
}
#endif