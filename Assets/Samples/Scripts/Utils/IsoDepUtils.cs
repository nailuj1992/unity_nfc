#if (!UNITY_EDITOR) && UNITY_ANDROID
using DigitsNFCToolkit.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitsNFCToolkit
{
    public class IsoDepUtils
    {
        public static readonly int NORMAL_MAX_TRANCEIVE_LENGTH = 256;
        public static readonly int DEFAULT_MAX_BLOCKSIZE = 224;

        public static readonly short EF_DG1 = 0x0101;
        public static readonly short EF_DG2 = 0x0102;
        public static readonly short EF_DG11 = 0x010B;

        private AndroidJavaObject TagObject { get; }
        private AndroidJavaObject Dep { get; set; }

        public IsoDepUtils()
        {
            TagObject = GetTagObject();

            Dep = GetIsoDepObject(TagObject);
            if (Dep == null)
            {
                throw new Exception("IsoDep is not supported by this Tag.");
            }
        }

        public AndroidJavaObject InitializeService()
        {
            // CardService cardService = CardService.getInstance(isoDep);
            AndroidJavaClass CardService = new AndroidJavaClass("net.sf.scuba.smartcards.CardService");
            AndroidJavaObject cardService = CardService.CallStatic<AndroidJavaObject>("getInstance", Dep);

            // cardService.open();
            cardService.Call("open");

            // PassportService service = new PassportService(cardService, NORMAL_MAX_TRANCEIVE_LENGTH, DEFAULT_MAX_BLOCKSIZE, true, false);
            AndroidJavaObject service = new AndroidJavaObject("org.jmrtd.PassportService", cardService, NORMAL_MAX_TRANCEIVE_LENGTH, DEFAULT_MAX_BLOCKSIZE, true, false);

            // service.open();
            service.Call("open");

            return service;
        }

        public void DoBACProcess(AndroidJavaObject service, string pnumber, string dateBirth, string expirationDate)
        {
            // service.sendSelectApplet(false);
            service.Call("sendSelectApplet", false);

            // BACKeySpec bacKey = new BACKey(passportNumber, birthDate, expirationDate);
            AndroidJavaObject bacKey = new AndroidJavaObject("org.jmrtd.BACKey", pnumber, dateBirth, expirationDate);

            // service.doBAC(bacKey);
            service.Call("doBACNow", bacKey);
        }

        public JSONObject GetPersonalDetails(AndroidJavaObject service)
        {
            // CardFileInputStream dg1In = service.getInputStream(PassportService.EF_DG1);
            AndroidJavaObject dgIn = service.Call<AndroidJavaObject>("getInputStream", EF_DG1);

            // DG1File dg1File = new DG1File(dg1In);
            AndroidJavaObject dgFile = new AndroidJavaObject("org.jmrtd.lds.icao.DG1File", dgIn);

            // MRZInfo mrzInfo = dg1File.getMRZInfo();
            AndroidJavaObject mrzInfo = dgFile.Call<AndroidJavaObject>("getMRZInfo");

            string secondaryIdentifier = mrzInfo.Call<string>("getSecondaryIdentifier").Replace("<", " ").Trim();
            string primaryIdentifier = mrzInfo.Call<string>("getPrimaryIdentifier").Replace("<", " ").Trim();
            string personalNumber = mrzInfo.Call<string>("getPersonalNumber");
            char gender = MRZUtils.ParseGender(mrzInfo.Call<AndroidJavaObject>("getGender").Call<int>("toInt"));
            string dateOfBirth = MRZUtils.ConvertFromMrzDate(mrzInfo.Call<string>("getDateOfBirth"));
            string dateOfExpiry = MRZUtils.ConvertFromMrzDate(mrzInfo.Call<string>("getDateOfExpiry"));
            string documentNumber = mrzInfo.Call<string>("getDocumentNumber");
            string nationality = mrzInfo.Call<string>("getNationality");
            string issuingState = mrzInfo.Call<string>("getIssuingState");
            string documentCode = mrzInfo.Call<string>("getDocumentCode");

            JSONObject resp = new JSONObject
            {
                { "secondaryIdentifier", secondaryIdentifier },
                { "primaryIdentifier", primaryIdentifier },
                { "personalNumber", personalNumber },
                { "gender", gender },
                { "dateOfBirth", dateOfBirth },
                { "dateOfExpiry", dateOfExpiry },
                { "documentNumber", documentNumber },
                { "nationality", nationality },
                { "issuingState", issuingState },
                { "documentCode", documentCode }
            };
            return resp;
        }

        public JSONObject GetFaceImage(AndroidJavaObject service, AndroidJavaObject context)
        {
            // CardFileInputStream dg2In = service.getInputStream(PassportService.EF_DG2);
            AndroidJavaObject dgIn = service.Call<AndroidJavaObject>("getInputStream", EF_DG2);

            // DG2File dg2File = new DG2File(dg2In);
            AndroidJavaObject dgFile = new AndroidJavaObject("org.jmrtd.lds.icao.DG2File", dgIn);

            // List<FaceInfo> faceInfos = dg2File.getFaceInfos();
            AndroidJavaObject faceInfos = dgFile.Call<AndroidJavaObject>("getFaceInfos");

            // List<FaceImageInfo> allFaceImageInfos = new ArrayList<>();
            List<AndroidJavaObject> allFaceImageInfos = new List<AndroidJavaObject>();

            // for (FaceInfo faceInfo : faceInfos) { allFaceImageInfos.addAll(faceInfo.getFaceImageInfos()); }
            for (int i = 0; i < faceInfos.Call<int>("size"); i++)
            {
                AndroidJavaObject faceInfo = faceInfos.Call<AndroidJavaObject>("get", i);
                AndroidJavaObject faceImageInfo = faceInfo.Call<AndroidJavaObject>("getFaceImageInfos");
                for (int j = 0; j < faceImageInfo.Call<int>("size"); j++)
                {
                    AndroidJavaObject info = faceImageInfo.Call<AndroidJavaObject>("get", i);
                    allFaceImageInfos.Add(info);
                }
            }

            JSONObject resp = new JSONObject();
            if (allFaceImageInfos.Count != 0)
            {
                // FaceImageInfo faceImageInfo = allFaceImageInfos.iterator().next();
                AndroidJavaObject faceImageInfo = allFaceImageInfos[0];

                // Image image = ImageUtil.getImage(MainActivity.this, faceImageInfo);

                string cacheDir = context.Call<AndroidJavaObject>("getCacheDir").Call<string>("getPath");

                AndroidJavaClass ImagesDecoder = new AndroidJavaClass("com.unity.images.ImagesDecoder");
                AndroidJavaObject image = ImagesDecoder.CallStatic<AndroidJavaObject>("getImage", cacheDir, faceImageInfo);

                // image.getBitmapImage();
                // image.getBase64Image();

                string base64ImageFace = image.Call<string>("getBase64Image");
                resp.Add("base64ImageFace", base64ImageFace);
            }
            return resp;
        }

        public JSONObject GetAdditionalDetails(AndroidJavaObject service)
        {
            try
            {
                // CardFileInputStream dg11In = service.getInputStream(PassportService.EF_DG11);
                AndroidJavaObject dgIn = service.Call<AndroidJavaObject>("getInputStream", EF_DG11);

                // DG11File dg11File = new DG11File(dg11In);
                AndroidJavaObject dgFile = new AndroidJavaObject("org.jmrtd.lds.icao.DG11File", dgIn);
                if (dgFile.Call<int>("getLength") > 0)// if (dg11File.getLength() > 0)
                {
                    string custodyInformation = dgFile.Call<string>("getCustodyInformation");
                    string nameOfHolder = dgFile.Call<string>("getNameOfHolder");
                    string fullDateOfBirth = dgFile.Call<string>("getFullDateOfBirth");
                    AndroidJavaObject otherNamesListJava = dgFile.Call<AndroidJavaObject>("getOtherNames");
                    AndroidJavaObject otherValidTDNumbersListJava = dgFile.Call<AndroidJavaObject>("getOtherValidTDNumbers");
                    AndroidJavaObject permanentAddressListJava = dgFile.Call<AndroidJavaObject>("getPermanentAddress");
                    string personalNumber = dgFile.Call<string>("getPersonalNumber");
                    string personalSummary = dgFile.Call<string>("getPersonalSummary");
                    AndroidJavaObject placeOfBirthListJava = dgFile.Call<AndroidJavaObject>("getPlaceOfBirth");
                    string profession = dgFile.Call<string>("getProfession");
                    string telephone = dgFile.Call<string>("getTelephone");
                    string title = dgFile.Call<string>("getTitle");

                    List<string> otherNames = new List<string>();
                    if (otherNamesListJava != null)
                    {
                        for (int i = 0; i < otherNamesListJava.Call<int>("size"); i++)
                        {
                            otherNames.Add(otherNamesListJava.Call<string>("get", i));
                        }
                    }

                    List<string> otherValidTDNumbers = new List<string>();
                    if (otherValidTDNumbersListJava != null)
                    {
                        for (int i = 0; i < otherValidTDNumbersListJava.Call<int>("size"); i++)
                        {
                            otherValidTDNumbers.Add(otherValidTDNumbersListJava.Call<string>("get", i));
                        }
                    }

                    List<string> permanentAddress = new List<string>();
                    if (permanentAddressListJava != null)
                    {
                        for (int i = 0; i < permanentAddressListJava.Call<int>("size"); i++)
                        {
                            permanentAddress.Add(permanentAddressListJava.Call<string>("get", i));
                        }
                    }

                    List<string> placeOfBirth = new List<string>();
                    if (placeOfBirthListJava != null)
                    {
                        for (int i = 0; i < placeOfBirthListJava.Call<int>("size"); i++)
                        {
                            placeOfBirth.Add(placeOfBirthListJava.Call<string>("get", i));
                        }
                    }

                    JSONObject resp = new JSONObject
                    {
                        { "custodyInformation", custodyInformation },
                        { "nameOfHolder",  nameOfHolder},
                        { "fullDateOfBirth",  fullDateOfBirth},
                        { "otherNames", JsonConvert.SerializeObject(otherNames) },
                        { "otherValidTDNumbers", JsonConvert.SerializeObject(otherValidTDNumbers) },
                        { "permanentAddress", JsonConvert.SerializeObject(permanentAddress) },
                        { "personalNumber", personalNumber },
                        { "personalSummary", personalSummary },
                        { "placeOfBirth", JsonConvert.SerializeObject(placeOfBirth) },
                        { "profession", profession },
                        { "telephone", telephone },
                        { "title", title }
                    };
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            return null;
        }

        private AndroidJavaObject GetTagObject()
        {
            // NativeNFC instance = NativeNFC.instance;
            AndroidJavaObject NativeNFCClass = new AndroidJavaClass("com.apollojourney.nativenfc.NativeNFC");
            AndroidJavaObject instance = NativeNFCClass.GetStatic<AndroidJavaObject>("instance");

            // Tag tagObject = instance.getLastTag();
            AndroidJavaObject tagObject = instance.Call<AndroidJavaObject>("getLastTag");
            return tagObject;
        }

        private AndroidJavaObject GetIsoDepObject(AndroidJavaObject tagObject)
        {
            // IsoDep dep = IsoDep.get(tagObject);
            AndroidJavaObject IsoDepClass = new AndroidJavaClass("android.nfc.tech.IsoDep");
            AndroidJavaObject dep = IsoDepClass.CallStatic<AndroidJavaObject>("get", tagObject);
            return dep;
        }
    }
}
#endif
