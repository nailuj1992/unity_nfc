using DigitsNFCToolkit.JSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public class ScanPassport : MonoBehaviour
    {
        private string MRZInfo { get; }
        private NFCTag Tag { get; }

        public ScanPassport(string MRZInfo, NFCTag Tag)
        {
            this.MRZInfo = MRZInfo;
            this.Tag = Tag;
        }

        public JSONObject GetPassportInfo()
        {
            JSONObject MRZJson = MRZUtils.ExtractMRZInfo(MRZInfo);

            if (MRZJson == null)
            {
                return null;
            }

#if (!UNITY_EDITOR) && UNITY_ANDROID
            if (Tag.IsIsoDep())
            {
                try
                {
                    IsoDepUtils isoDepUtils = new IsoDepUtils();

                    // Get the Unity Player class
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

                    // Get the current activity
                    AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                    // Get the context for the activity
                    AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

                    string pnumber = MRZUtils.FixDocumentNumber(MRZJson.GetString("pnumber"));
                    string dateBirth = MRZJson.GetString("dateBirth");
                    string expirationDate = MRZJson.GetString("expirationDate");

                    AndroidJavaObject service = isoDepUtils.InitializeService();
                    isoDepUtils.DoBACProcess(service, pnumber, dateBirth, expirationDate);

                    // -- Personal Details -- //
                    JSONObject personalInfo = isoDepUtils.GetPersonalDetails(service);

                    // -- Face Image -- //
                    JSONObject faceImage = isoDepUtils.GetFaceImage(service, context);

                    // -- Additional Details (if exist) -- //
                    JSONObject additionalInfo = isoDepUtils.GetAdditionalDetails(service);

                    JSONObject passportInfo = new JSONObject
                    {
                        { "personalInfo", personalInfo },
                        { "additionalInfo", additionalInfo },
                        { "faceImage", faceImage }
                    };

                    isoDepUtils.CloseService(service);

                    return passportInfo;
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                }
            }
#endif
            return null;
        }
    }
}
