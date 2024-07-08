#if UNITY_IOS
using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Implementations.iOS.Internal;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.iOS
{
    internal class BarcodeResultParser
    {
        float m_displayWidth;
        float m_displayHeight;
        float m_inputWidth;
        float m_inputHeight;
        float m_inputRotation;
        NativeArray m_nativeBarcodes;

        public BarcodeResultParser(NativeArray nativeBarcodes, float displayWidth, float displayHeight, float inputWidth, float inputHeight, float inputRotation)
        {
            m_nativeBarcodes = nativeBarcodes;
            m_displayWidth = displayWidth;
            m_displayHeight = displayHeight;
            m_inputWidth = inputWidth;
            m_inputHeight = inputHeight;
            m_inputRotation = inputRotation;
        }

        public List<Barcode> GetResult()
        {
            return GetBarcodes(m_nativeBarcodes.GetStructArray<NativeBarcode>());
        }

        private List<Barcode> GetBarcodes(NativeBarcode[] nativeBarcodes)
        {
            if (nativeBarcodes != null)
            {
                List<Barcode> barcodes = new List<Barcode>();

                foreach (NativeBarcode nativeBarcode in nativeBarcodes)
                {
                    Barcode barcode = ConvertFromNativeBarcode(nativeBarcode);
                    barcodes.Add(barcode);
                }

                return barcodes;
            }
            else
            {
                return null;
            }

        }

        private Barcode ConvertFromNativeBarcode(NativeBarcode source)
        {
            BarcodeFormat barcodeFormat = (BarcodeFormat)source.Format;
            object value = null;
            BarcodeValueType valueType;
            switch (source.ValueType)
            {
                case NativeBarcodeValueType.NativeBarcodeValueTypeUnknown:
                    valueType = BarcodeValueType.UNKNOWN;
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeContactInfo:
                    valueType = BarcodeValueType.CONTACT_INFO;
                    NativeBarcodeContactInfo sourceContactInfo = source.ContactInfo;
                    List<Barcode.Address> addresses = GetAddresses(sourceContactInfo.Addresses.GetStructArray<NativeBarcodeAddress>());
                    List<Barcode.Email> emails = GetEmails(sourceContactInfo.Emails.GetStructArray<NativeBarcodeEmail>());
                    List<Barcode.Phone> phones = GetPhones(sourceContactInfo.Phones.GetStructArray<NativeBarcodePhone>());
                    List<string> urls = GetUrls(sourceContactInfo.Phones);
                    value = new Barcode.ContactInfo(addresses, emails, GetPersonName(sourceContactInfo.Name), sourceContactInfo.Organisation.AsString(), phones, sourceContactInfo.JobTitle.AsString(), urls);
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeEmail:
                    valueType = BarcodeValueType.EMAIL;
                    value = GetEmail(source.Email);
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeISBN:
                    valueType = BarcodeValueType.ISBN;
                    value = source.RawValue.AsString();
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypePhone:
                    valueType = BarcodeValueType.PHONE;
                    value = GetPhone(source.Phone);
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeProduct:
                    valueType = BarcodeValueType.PRODUCT;
                    value = source.RawValue.AsString();
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeSMS:
                    valueType = BarcodeValueType.SMS;
                    value = new Barcode.Sms(source.Sms.Message.AsString(), source.Sms.PhoneNumber.AsString());
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeText:
                    valueType = BarcodeValueType.TEXT;
                    value = source.RawValue.AsString();
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeURL:
                    valueType = BarcodeValueType.URL;
                    value = new Barcode.UrlBookmark(source.UrlBookmark.Title.AsString(), source.UrlBookmark.Url.AsString());
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeWiFi:
                    valueType = BarcodeValueType.WIFI;
                    value = new Barcode.Wifi((Barcode.Wifi.WifiEncryptionType)source.Wifi.EncryptionType, source.Wifi.Password.AsString(), source.Wifi.Ssid.AsString());
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeGeographicCoordinates:
                    valueType = BarcodeValueType.GEO;
                    value = new Barcode.GeoPoint(source.GeoPoint.Latitude, source.GeoPoint.Longitude);
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeCalendarEvent:
                    valueType = BarcodeValueType.CALENDAR_EVENT;
                    NativeBarcodeCalendarEvent nativeCalendarEvent = source.CalendarEvent;
                    value = new Barcode.CalendarEvent(nativeCalendarEvent.StartTimestamp, nativeCalendarEvent.EndTimestamp, nativeCalendarEvent.Summary.AsString(), nativeCalendarEvent.Summary.AsString(), nativeCalendarEvent.Location.AsString(), nativeCalendarEvent.Organizer.AsString(), nativeCalendarEvent.Status.AsString());
                    break;
                case NativeBarcodeValueType.NativeBarcodeValueTypeDriversLicense:
                    valueType = BarcodeValueType.DRIVER_LICENSE;
                    value = GetDrivingLicense(source.DrivingLicense);
                    break;
                default:
                    //throw new Exception("Value type not implemented");
                    valueType = BarcodeValueType.UNKNOWN;
                    DebugLogger.LogWarning("Value type not implemented : " + source.ValueType + " Raw Value : " + source.RawValue);
                    break;
            }

            return new Barcode(barcodeFormat, valueType, value, source.RawValue.AsString(), source.RawBytes != IntPtr.Zero ? Convert.FromBase64String(source.RawBytes.AsString()) : null, source.DisplayValue.AsString(), GetRect(source.Frame));
        }

        private List<Barcode.Address> GetAddresses(NativeBarcodeAddress[] source)
        {
            if (source == null)
                return null;

            List<Barcode.Address> addresses = new List<Barcode.Address>();
            foreach (NativeBarcodeAddress each in source)
            {
                Barcode.Address address = new Barcode.Address((Barcode.Address.AddressType)each.AddressType, each.AddressLines.GetStringArray());
                addresses.Add(address);
            }

            return addresses;
        }

        private List<Barcode.Email> GetEmails(NativeBarcodeEmail[] source)
        {
            if (source == null)
                return null;

            List<Barcode.Email> emails = new List<Barcode.Email>();
            foreach (NativeBarcodeEmail each in source)
            {
                Barcode.Email email = GetEmail(each);
                emails.Add(email);
            }

            return emails;
        }

        private Barcode.Email GetEmail(NativeBarcodeEmail nativeEmail)
        {
            return new Barcode.Email((Barcode.Email.EmailType)nativeEmail.EmailType, nativeEmail.Address.AsString(), nativeEmail.Subject.AsString(), nativeEmail.Body.AsString());
        }

        private List<Barcode.Phone> GetPhones(NativeBarcodePhone[] source)
        {
            if (source == null)
                return null;

            List<Barcode.Phone> phones = new List<Barcode.Phone>();
            foreach (NativeBarcodePhone each in source)
            {
                Barcode.Phone phone = new Barcode.Phone((Barcode.Phone.PhoneType)each.PhoneType, each.Number.AsString());
                phones.Add(phone);
            }

            return phones;
        }

        private Barcode.PersonName GetPersonName(NativeBarcodePersonName source)
        {
            Barcode.PersonName personName = new Barcode.PersonName(first: source.First.AsString(),
                                                                    middle: source.Middle.AsString(),
                                                                    last: source.Last.AsString(),
                                                                    prefix: source.Prefix.AsString(),
                                                                    suffix: source.Suffix.AsString(),
                                                                    pronunciation: source.Pronunciation.AsString(),
                                                                    formattedName: source.FormattedName.AsString());
            return personName;
        }

        private Barcode.Phone GetPhone(NativeBarcodePhone nativePhone)
        {
            Barcode.Phone phone = new Barcode.Phone((Barcode.Phone.PhoneType)nativePhone.PhoneType, nativePhone.Number.AsString());
            return phone;
        }

        private Barcode.DrivingLicense GetDrivingLicense(NativeBarcodeDrivingLicense source)
        {
            return new Barcode.DrivingLicense(source.DocumentType.AsString(),
                                                source.BirthTimestamp,
                                                source.ExpiryTimestamp,
                                                source.IssueTimestamp,
                                                source.IssuingCountry.AsString(),
                                                source.LicenseNumber.AsString(),
                                                source.FirstName.AsString(),
                                                source.MiddleName.AsString(),
                                                source.LastName.AsString(),
                                                source.Gender.AsString(),
                                                source.AddressCity.AsString(),
                                                source.AddressStreet.AsString(),
                                                source.AddressState.AsString(),
                                                source.AddressZip.AsString());
        }


        private List<string> GetUrls(NativeArray stringArray)
        {
            string[] urls = MarshalUtility.CreateStringArray(stringArray.Pointer, stringArray.Length);
            if (urls == null || urls.Length == 0)
                return null;

            List<string> urlsList = new List<string>(urls);
            return urlsList;
        }


        private Rect GetRect(UnityRect nativeRect)
        {   
            Rect rect = new Rect(nativeRect.X, nativeRect.Y, nativeRect.Width, nativeRect.Height);
            Rect transformedRect = InputSourceUtility.TransformRectToUserSpace(rect, m_displayWidth, m_displayHeight, m_inputWidth, m_inputHeight, m_inputRotation);
            return transformedRect;
        }
    }
}
#endif