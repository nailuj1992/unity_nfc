#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDriverLicense : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDriverLicense(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDriverLicense(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeDriverLicense()
        {
            DebugLogger.Log("Disposing NativeDriverLicense");
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

        public string GetAddressCity()
        {
            return Call<string>(Native.Method.kGetAddressCity);
        }
        public string GetAddressState()
        {
            return Call<string>(Native.Method.kGetAddressState);
        }
        public string GetAddressStreet()
        {
            return Call<string>(Native.Method.kGetAddressStreet);
        }
        public string GetAddressZip()
        {
            return Call<string>(Native.Method.kGetAddressZip);
        }
        public string GetBirthDate()
        {
            return Call<string>(Native.Method.kGetBirthDate);
        }
        public string GetDocumentType()
        {
            return Call<string>(Native.Method.kGetDocumentType);
        }
        public string GetExpiryDate()
        {
            return Call<string>(Native.Method.kGetExpiryDate);
        }
        public string GetFirstName()
        {
            return Call<string>(Native.Method.kGetFirstName);
        }
        public string GetGender()
        {
            return Call<string>(Native.Method.kGetGender);
        }
        public string GetIssueDate()
        {
            return Call<string>(Native.Method.kGetIssueDate);
        }
        public string GetIssuingCountry()
        {
            return Call<string>(Native.Method.kGetIssuingCountry);
        }
        public string GetLastName()
        {
            return Call<string>(Native.Method.kGetLastName);
        }
        public string GetLicenseNumber()
        {
            return Call<string>(Native.Method.kGetLicenseNumber);
        }
        public string GetMiddleName()
        {
            return Call<string>(Native.Method.kGetMiddleName);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.barcode.common.Barcode$DriverLicense";

            internal class Method
            {
                internal const string kGetIssueDate = "getIssueDate";
                internal const string kGetFirstName = "getFirstName";
                internal const string kGetBirthDate = "getBirthDate";
                internal const string kGetMiddleName = "getMiddleName";
                internal const string kGetAddressZip = "getAddressZip";
                internal const string kGetExpiryDate = "getExpiryDate";
                internal const string kGetLastName = "getLastName";
                internal const string kGetGender = "getGender";
                internal const string kGetDocumentType = "getDocumentType";
                internal const string kGetAddressState = "getAddressState";
                internal const string kGetAddressCity = "getAddressCity";
                internal const string kGetAddressStreet = "getAddressStreet";
                internal const string kGetLicenseNumber = "getLicenseNumber";
                internal const string kGetIssuingCountry = "getIssuingCountry";
            }

        }
    }
}
#endif