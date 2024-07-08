#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeCalendarDateTime : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeCalendarDateTime(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeCalendarDateTime(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeCalendarDateTime()
        {
            DebugLogger.Log("Disposing NativeCalendarDateTime");
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

        public int GetDay()
        {
            return Call<int>(Native.Method.kGetDay);
        }
        public int GetHours()
        {
            return Call<int>(Native.Method.kGetHours);
        }
        public int GetMinutes()
        {
            return Call<int>(Native.Method.kGetMinutes);
        }
        public int GetMonth()
        {
            return Call<int>(Native.Method.kGetMonth);
        }
        public string GetRawValue()
        {
            return Call<string>(Native.Method.kGetRawValue);
        }
        public int GetSeconds()
        {
            return Call<int>(Native.Method.kGetSeconds);
        }
        public int GetYear()
        {
            return Call<int>(Native.Method.kGetYear);
        }
        public bool IsUtc()
        {
            return Call<bool>(Native.Method.kIsUtc);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$CalendarDateTime";

            internal class Method
            {
                internal const string kGetHours = "getHours";
                internal const string kGetMonth = "getMonth";
                internal const string kGetRawValue = "getRawValue";
                internal const string kGetYear = "getYear";
                internal const string kGetMinutes = "getMinutes";
                internal const string kGetSeconds = "getSeconds";
                internal const string kGetDay = "getDay";
                internal const string kIsUtc = "isUtc";
            }

        }
    }
}
#endif