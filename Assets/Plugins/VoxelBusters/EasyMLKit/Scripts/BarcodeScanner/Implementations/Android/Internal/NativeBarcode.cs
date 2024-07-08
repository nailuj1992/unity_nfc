#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeBarcode : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion
        #region Public properties

        public const int FORMAT_UNKNOWN = -1;

        public const int FORMAT_ALL_FORMATS = 0;

        public const int FORMAT_CODE_128 = 1;

        public const int FORMAT_CODE_39 = 2;

        public const int FORMAT_CODE_93 = 4;

        public const int FORMAT_CODABAR = 8;

        public const int FORMAT_DATA_MATRIX = 16;

        public const int FORMAT_EAN_13 = 32;

        public const int FORMAT_EAN_8 = 64;

        public const int FORMAT_ITF = 128;

        public const int FORMAT_QR_CODE = 256;

        public const int FORMAT_UPC_A = 512;

        public const int FORMAT_UPC_E = 1024;

        public const int FORMAT_PDF417 = 2048;

        public const int FORMAT_AZTEC = 4096;

        public const int TYPE_UNKNOWN = 0;

        public const int TYPE_CONTACT_INFO = 1;

        public const int TYPE_EMAIL = 2;

        public const int TYPE_ISBN = 3;

        public const int TYPE_PHONE = 4;

        public const int TYPE_PRODUCT = 5;

        public const int TYPE_SMS = 6;

        public const int TYPE_TEXT = 7;

        public const int TYPE_URL = 8;

        public const int TYPE_WIFI = 9;

        public const int TYPE_GEO = 10;

        public const int TYPE_CALENDAR_EVENT = 11;

        public const int TYPE_DRIVER_LICENSE = 12;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeBarcode(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeBarcode(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeBarcode()
        {
            DebugLogger.Log("Disposing NativeBarcode");
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
        public NativeCalendarEvent GetCalendarEvent()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetCalendarEvent);
            if(nativeObj != null)
            {
                NativeCalendarEvent data  = new  NativeCalendarEvent(nativeObj);
                return data;
            }
            else
            {
                return default(NativeCalendarEvent);
            }
        }
        public NativeContactInfo GetContactInfo()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetContactInfo);
            if(nativeObj != null)
            {
                NativeContactInfo data  = new  NativeContactInfo(nativeObj);
                return data;
            }
            else
            {
                return default(NativeContactInfo);
            }
        }
        public NativePoint[] GetCornerPoints()
        {
            AndroidJavaObject[] nativeObjs = Call<AndroidJavaObject[]>(Native.Method.kGetCornerPoints);
            NativePoint[] data  = new  NativePoint[nativeObjs.Length];
            for(int i=0; i<nativeObjs.Length; i++)
            {
                if(nativeObjs[i] != null)
                {
                    data[i] = new NativePoint(nativeObjs[i]);
                }
                else
                {
                    data[i] = null;
                }
            }
            return data;
        }
        public string GetDisplayValue()
        {
            return Call<string>(Native.Method.kGetDisplayValue);
        }
        public NativeDriverLicense GetDriverLicense()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetDriverLicense);
            if(nativeObj != null)
            {
                NativeDriverLicense data  = new  NativeDriverLicense(nativeObj);
                return data;
            }
            else
            {
                return default(NativeDriverLicense);
            }
        }
        public NativeEmail GetEmail()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetEmail);
            if(nativeObj != null)
            {
                NativeEmail data  = new  NativeEmail(nativeObj);
                return data;
            }
            else
            {
                return default(NativeEmail);
            }
        }
        public int GetFormat()
        {
            return Call<int>(Native.Method.kGetFormat);
        }
        public NativeGeoPoint GetGeoPoint()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetGeoPoint);
            if(nativeObj != null)
            {
                NativeGeoPoint data  = new  NativeGeoPoint(nativeObj);
                return data;
            }
            else
            {
                return default(NativeGeoPoint);
            }
        }
        public NativePhone GetPhone()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetPhone);
            if(nativeObj != null)
            {
                NativePhone data  = new  NativePhone(nativeObj);
                return data;
            }
            else
            {
                return default(NativePhone);
            }
        }
        public sbyte[] GetRawBytes()
        {
            return Call<sbyte[]>(Native.Method.kGetRawBytes);
        }
        public string GetRawValue()
        {
            return Call<string>(Native.Method.kGetRawValue);
        }
        public NativeSms GetSms()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetSms);
            if(nativeObj != null)
            {
                NativeSms data  = new  NativeSms(nativeObj);
                return data;
            }
            else
            {
                return default(NativeSms);
            }
        }
        public NativeUrlBookmark GetUrl()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetUrl);
            if(nativeObj != null)
            {
                NativeUrlBookmark data  = new  NativeUrlBookmark(nativeObj);
                return data;
            }
            else
            {
                return default(NativeUrlBookmark);
            }
        }
        public int GetValueType()
        {
            return Call<int>(Native.Method.kGetValueType);
        }
        public NativeWiFi GetWifi()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetWifi);
            if(nativeObj != null)
            {
                NativeWiFi data  = new  NativeWiFi(nativeObj);
                return data;
            }
            else
            {
                return default(NativeWiFi);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode";

            internal class Method
            {
                internal const string kGetEmail = "getEmail";
                internal const string kGetPhone = "getPhone";
                internal const string kGetValueType = "getValueType";
                internal const string kGetRawBytes = "getRawBytes";
                internal const string kGetGeoPoint = "getGeoPoint";
                internal const string kGetRawValue = "getRawValue";
                internal const string kGetWifi = "getWifi";
                internal const string kGetFormat = "getFormat";
                internal const string kGetContactInfo = "getContactInfo";
                internal const string kGetCornerPoints = "getCornerPoints";
                internal const string kGetBoundingBox = "getBoundingBox";
                internal const string kGetDisplayValue = "getDisplayValue";
                internal const string kGetUrl = "getUrl";
                internal const string kGetSms = "getSms";
                internal const string kGetCalendarEvent = "getCalendarEvent";
                internal const string kGetDriverLicense = "getDriverLicense";
            }

        }
    }
}
#endif