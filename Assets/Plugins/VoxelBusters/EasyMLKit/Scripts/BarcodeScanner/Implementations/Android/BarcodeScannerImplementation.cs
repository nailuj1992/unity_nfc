#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.Implementations.Android.Internal;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android
{
    public class BarcodeScannerImplementation : NativeImageBasedInputFeatureBase, IBarcodeScannerImplementation
    {
        #region Private fields
        
        private bool m_initialised;
        private NativeGoogleMLKitBarcodeScanner m_instance;
        private OnPrepareCompleteInternalCallback m_prepareCompleteCallback;
        private OnProcessUpdateInternalCallback<BarcodeScannerResult> m_processUpdateCallback;
        private OnCloseInternalCallback m_closeCallback;

        public bool IsAvailable => true;

        public NativeObjectRef NativeObjectRef => throw new NotImplementedException();

        #endregion

        public BarcodeScannerImplementation() : base()
        {
            m_instance = new NativeGoogleMLKitBarcodeScanner(NativeUnityPluginUtility.GetContext());
        }

        public void Prepare(IImageInputSource inputSource, BarcodeScannerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (!m_initialised)
            {
                Initialise();
            }

            base.Prepare(inputSource);
            m_prepareCompleteCallback = callback;
            m_instance.Prepare(GetNativeInputImageProducer(inputSource), CreateNativeOptions(options));
        }

        public void Process(OnProcessUpdateInternalCallback<BarcodeScannerResult> callback)
        {
            m_processUpdateCallback = callback;
            m_instance.Process();
        }

        public void Close(OnCloseInternalCallback callback)
        {
            m_closeCallback = callback;
            m_instance.Close();

            if(m_closeCallback != null)
            {
                m_closeCallback(null);
            }
        }

        private void Initialise()
        {
            SetupListener();
            m_initialised = true;
        }

        
        private void SetupListener()
        {
            m_instance.SetListener(new NativeGoogleMLKitBarcodeScannerListener()
            {
                onScanSuccessCallback = (NativeList<NativeBarcode> nativeBarcodes, NativeInputImageInfo inputDimensions) =>
                {
                    if (m_processUpdateCallback != null)
                    {
                        List<Barcode> barcodes = GetBarcodes(nativeBarcodes.Get(), inputDimensions);
                        Callback callback = () => m_processUpdateCallback(new BarcodeScannerResult(barcodes, null));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onScanFailedCallback = (NativeException exception) =>
                {
                    if (m_processUpdateCallback != null)
                    {
                        Callback callback = () => m_processUpdateCallback(new BarcodeScannerResult(null, new Error(exception.GetMessage())));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onPrepareSuccessCallback = () =>
                {
                    if (m_prepareCompleteCallback != null)
                    {
                        Callback callback = () => m_prepareCompleteCallback(null);
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onPrepareFailedCallback = (NativeException exception) =>
                {
                    if (m_prepareCompleteCallback != null)
                    {
                        Callback callback = () => m_prepareCompleteCallback(new Error(exception.GetMessage()));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                }
            });
        }

        private List<Barcode> GetBarcodes(List<NativeBarcode> nativeBarcodes, NativeInputImageInfo dimensions)
        {
            List<Barcode> barcodes = new List<Barcode>();

            foreach(NativeBarcode nativeBarcode in nativeBarcodes)
            {
                Barcode barcode = ConvertFromNativeBarcode(nativeBarcode, dimensions);
                barcodes.Add(barcode);
            }

            return barcodes;
        }

        private Barcode ConvertFromNativeBarcode(NativeBarcode source, NativeInputImageInfo dimensions)
        {
            BarcodeFormat barcodeFormat = (BarcodeFormat) source.GetFormat();
            object value = null;
            BarcodeValueType valueType;

            switch (source.GetValueType())
            {
                case NativeBarcode.TYPE_UNKNOWN:
                    valueType = BarcodeValueType.UNKNOWN;
                    break;
                case NativeBarcode.TYPE_CONTACT_INFO:
                    valueType = BarcodeValueType.CONTACT_INFO;
                    NativeContactInfo sourceContactInfo = source.GetContactInfo();
                    List<Barcode.Address> addresses = GetAddresses(sourceContactInfo.GetAddresses().Get());
                    List<Barcode.Email> emails = GetEmails(sourceContactInfo.GetEmails().Get());
                    List<Barcode.Phone> phones = GetPhones(sourceContactInfo.GetPhones().Get());
                    value = new Barcode.ContactInfo(addresses, emails, GetPersonName(sourceContactInfo.GetName()), sourceContactInfo.GetOrganization(), phones, sourceContactInfo.GetTitle(), sourceContactInfo.GetUrls().Get());
                    break;
                case NativeBarcode.TYPE_EMAIL:
                    valueType = BarcodeValueType.EMAIL;
                    value = GetEmail(source.GetEmail());
                    break;
                case NativeBarcode.TYPE_ISBN:
                    valueType = BarcodeValueType.ISBN;
                    value = source.GetRawValue();
                    break;
                case NativeBarcode.TYPE_PHONE:
                    valueType = BarcodeValueType.PHONE;
                    value = GetPhone(source.GetPhone());
                    break;
                case NativeBarcode.TYPE_PRODUCT:
                    valueType = BarcodeValueType.PRODUCT;
                    value = source.GetRawValue();
                    break;
                case NativeBarcode.TYPE_SMS:
                    valueType = BarcodeValueType.SMS;
                    value = new Barcode.Sms(source.GetSms().GetMessage(), source.GetSms().GetPhoneNumber());
                    break;
                case NativeBarcode.TYPE_TEXT:
                    valueType = BarcodeValueType.TEXT;
                    value = source.GetRawValue();
                    break;
                case NativeBarcode.TYPE_URL:
                    valueType = BarcodeValueType.URL;
                    value = new Barcode.UrlBookmark(source.GetUrl().GetTitle(), source.GetUrl().GetUrl());
                    break;
                case NativeBarcode.TYPE_WIFI:
                    valueType = BarcodeValueType.WIFI;
                    value = new Barcode.Wifi((Barcode.Wifi.WifiEncryptionType)source.GetWifi().GetEncryptionType(), source.GetWifi().GetPassword(), source.GetWifi().GetSsid());
                    break;
                case NativeBarcode.TYPE_GEO:
                    valueType = BarcodeValueType.GEO;
                    value = new Barcode.GeoPoint(source.GetGeoPoint().GetLat(), source.GetGeoPoint().GetLng());
                    break;
                case NativeBarcode.TYPE_CALENDAR_EVENT:
                    valueType = BarcodeValueType.CALENDAR_EVENT;
                    NativeCalendarEvent nativeCalendarEvent = source.GetCalendarEvent();
                    value = new Barcode.CalendarEvent(ToDateTimeEpoch(nativeCalendarEvent.GetStart()), ToDateTimeEpoch(nativeCalendarEvent.GetEnd()), nativeCalendarEvent.GetDescription(), nativeCalendarEvent.GetSummary(), nativeCalendarEvent.GetLocation(), nativeCalendarEvent.GetOrganizer(), nativeCalendarEvent.GetStatus());
                    break;
                case NativeBarcode.TYPE_DRIVER_LICENSE:
                    valueType = BarcodeValueType.DRIVER_LICENSE;
                    value = GetDrivingLicense(source.GetDriverLicense());
                    break;
                default:
                    throw new Exception("Value type not implemented");
            }

            return new Barcode(barcodeFormat, valueType, value, source.GetRawValue(), (byte[])(Array)source.GetRawBytes(), source.GetDisplayValue(), GetRect(source.GetBoundingBox(), dimensions));
        }

        private List<Barcode.Address> GetAddresses(List<NativeAddress> source)
        {
            List<Barcode.Address> addresses = new List<Barcode.Address>();
            foreach(NativeAddress each in source)
            {
                Barcode.Address address = new Barcode.Address((Barcode.Address.AddressType) each.GetType(), each.GetAddressLines());
                addresses.Add(address);
            }

            return addresses;
        }

        private List<Barcode.Email> GetEmails(List<NativeEmail> source)
        {
            List<Barcode.Email> emails = new List<Barcode.Email>();
            foreach (NativeEmail each in source)
            {
                Barcode.Email email = GetEmail(each);
                emails.Add(email);
            }

            return emails;
        }

        private Barcode.Email GetEmail(NativeEmail nativeEmail)
        {
            return new Barcode.Email((Barcode.Email.EmailType)nativeEmail.GetType(), nativeEmail.GetAddress(), nativeEmail.GetSubject(), nativeEmail.GetBody());
        }

        private List<Barcode.Phone> GetPhones(List<NativePhone> source)
        {
            List<Barcode.Phone> phones = new List<Barcode.Phone>();
            foreach (NativePhone each in source)
            {
                Barcode.Phone phone = new Barcode.Phone((Barcode.Phone.PhoneType)each.GetType(), each.GetNumber());
                phones.Add(phone);
            }

            return phones;
        }

        private Barcode.PersonName GetPersonName(NativePersonName source)
        {
            Barcode.PersonName personName = new Barcode.PersonName(first: source.GetFirst(),
                                                                    middle: source.GetMiddle(),
                                                                    last: source.GetLast(),
                                                                    prefix: source.GetPrefix(),
                                                                    suffix: source.GetSuffix(),
                                                                    pronunciation: source.GetPronunciation(),
                                                                    formattedName: source.GetFormattedName());
            return personName;
        }

        private Barcode.Phone GetPhone(NativePhone nativePhone)
        {
            Barcode.Phone phone = new Barcode.Phone((Barcode.Phone.PhoneType)nativePhone.GetType(), nativePhone.GetNumber());
            return phone;
        }

        private List<Barcode.UrlBookmark> GetUrls(List<NativeUrlBookmark> source)
        {
            List<Barcode.UrlBookmark> urls = new List<Barcode.UrlBookmark>();
            foreach (NativeUrlBookmark each in source)
            {
                Barcode.UrlBookmark url = new Barcode.UrlBookmark(each.GetTitle(), each.GetUrl());
                urls.Add(url);
            }

            return urls;
        }

        private Barcode.DrivingLicense GetDrivingLicense(NativeDriverLicense source)
        {
            return new Barcode.DrivingLicense(source.GetDocumentType(),
                                                ToEpoch(DateTime.Parse(source.GetBirthDate())),
                                                ToEpoch(DateTime.Parse(source.GetExpiryDate())),
                                                ToEpoch(DateTime.Parse(source.GetIssueDate())),
                                                source.GetIssuingCountry(),
                                                source.GetLicenseNumber(),
                                                source.GetFirstName(),
                                                source.GetMiddleName(),
                                                source.GetLastName(),
                                                source.GetGender(),
                                                source.GetAddressCity(),
                                                source.GetAddressStreet(),
                                                source.GetAddressState(),
                                                source.GetAddressZip());
        }


        private long ToEpoch(DateTime dateTime)
        {
            TimeSpan t = dateTime - new DateTime(1970, 1, 1);
            return (long)t.TotalMilliseconds;
        }

        private DateTime GetDateTime(NativeCalendarDateTime source)
        {
            DateTime dateTime = new DateTime(source.GetYear(), source.GetMonth(), source.GetDay(), source.GetHours(), source.GetMinutes(), source.GetSeconds(), 0, CultureInfo.InvariantCulture.Calendar, source.IsUtc() ? DateTimeKind.Utc : DateTimeKind.Local);
            return dateTime;
        }

        private long ToDateTimeEpoch(NativeCalendarDateTime source)
        {
            DateTime dateTime = GetDateTime(source);
            return ToEpoch(dateTime);
        }

        private Rect GetRect(NativeRect nativeRect, NativeInputImageInfo imageInfo)
        {
            Rect rawRect = new Rect(nativeRect.Left, nativeRect.Top, nativeRect.Right - nativeRect.Left, nativeRect.Bottom - nativeRect.Top);
            return InputSourceUtility.TransformRectToUserSpace(rawRect, m_inputSource.GetWidth(), m_inputSource.GetHeight(), imageInfo.GetWidth(), imageInfo.GetHeight(), imageInfo.GetRotation());
        }

        private int GetAllowedFormats(BarcodeFormat scannableFormats)
        {
            return (int)scannableFormats;//Both native format int values and c# values match. So we can pass directly.
        }

        private NativeBarcodeScanOptions CreateNativeOptions(BarcodeScannerOptions options)
        {
            NativeBarcodeScanOptions nativeOptions = new NativeBarcodeScanOptions();
            nativeOptions.SetAllowedFormats(GetAllowedFormats(options.ScannableFormats));
            return nativeOptions;
        }

        public IntPtr AddrOfNativeObject()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
#endif
