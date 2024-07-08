#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeContactInfo : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeContactInfo(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeContactInfo(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeContactInfo()
        {
            DebugLogger.Log("Disposing NativeContactInfo");
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

        public NativeList<NativeAddress> GetAddresses()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetAddresses);
            if(nativeObj != null)
            {
                NativeList<NativeAddress> data  = new  NativeList<NativeAddress>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeAddress>);
            }
        }
        public NativeList<NativeEmail> GetEmails()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetEmails);
            if(nativeObj != null)
            {
                NativeList<NativeEmail> data  = new  NativeList<NativeEmail>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeEmail>);
            }
        }
        public NativePersonName GetName()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetName);
            if(nativeObj != null)
            {
                NativePersonName data  = new  NativePersonName(nativeObj);
                return data;
            }
            else
            {
                return default(NativePersonName);
            }
        }
        public string GetOrganization()
        {
            return Call<string>(Native.Method.kGetOrganization);
        }
        public NativeList<NativePhone> GetPhones()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetPhones);
            if(nativeObj != null)
            {
                NativeList<NativePhone> data  = new  NativeList<NativePhone>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativePhone>);
            }
        }
        public string GetTitle()
        {
            return Call<string>(Native.Method.kGetTitle);
        }
        public NativeList<string> GetUrls()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetUrls);
            if(nativeObj != null)
            {
                NativeList<string> data  = new  NativeList<string>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<string>);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$ContactInfo";

            internal class Method
            {
                internal const string kGetTitle = "getTitle";
                internal const string kGetAddresses = "getAddresses";
                internal const string kGetName = "getName";
                internal const string kGetUrls = "getUrls";
                internal const string kGetPhones = "getPhones";
                internal const string kGetEmails = "getEmails";
                internal const string kGetOrganization = "getOrganization";
            }

        }
    }
}
#endif