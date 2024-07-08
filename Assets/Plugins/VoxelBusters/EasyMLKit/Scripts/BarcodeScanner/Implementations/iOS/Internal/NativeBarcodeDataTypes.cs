#if UNITY_IOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal enum NativeBarcodeEmailType
    {
        NativeBarcodeEmailTypeUnknown = 0,
        NativeBarcodeEmailTypeWork = 1,
        NativeBarcodeEmailTypeHome = 2
    }

    internal enum NativeBarcodePhoneType
    {
        NativeBarcodePhoneTypeUnknown = 0,
        NativeBarcodePhoneTypeWork = 1,
        NativeBarcodePhoneTypeHome = 2,
        NativeBarcodePhoneTypeFax = 3,
        NativeBarcodePhoneTypeMobile = 4
    };

    internal enum NativeBarcodeFormat
    {
        /** Unknown format. */
        NativeBarcodeFormatUnknown = 0,
        /** All format. */
        NativeBarcodeFormatAll = 0xFFFF,
        /** Code-128 detection. */
        NativeBarcodeFormatCode128 = 0x0001,
        /** Code-39 detection. */
        NativeBarcodeFormatCode39 = 0x0002,
        /** Code-93 detection. */
        NativeBarcodeFormatCode93 = 0x0004,
        /** Codabar detection. */
        NativeBarcodeFormatCodaBar = 0x0008,
        /** Data Matrix detection. */
        NativeBarcodeFormatDataMatrix = 0x0010,
        /** EAN-13 detection. */
        NativeBarcodeFormatEAN13 = 0x0020,
        /** EAN-8 detection. */
        NativeBarcodeFormatEAN8 = 0x0040,
        /** ITF detection. */
        NativeBarcodeFormatITF = 0x0080,
        /** QR Code detection. */
        NativeBarcodeFormatQRCode = 0x0100,
        /** UPC-A detection. */
        NativeBarcodeFormatUPCA = 0x0200,
        /** UPC-E detection. */
        NativeBarcodeFormatUPCE = 0x0400,
        /** PDF-417 detection. */
        NativeBarcodeFormatPDF417 = 0x0800,
        /** Aztec code detection. */
        NativeBarcodeFormatAztec = 0x1000,
    };

    internal enum NativeBarcodeValueType
    {
        /** Unknown format. */
        NativeBarcodeValueTypeUnknown = 0,
        /** All format. */
        NativeBarcodeValueTypeContactInfo = 1,
        /** Code-128 detection. */
        NativeBarcodeValueTypeEmail = 2,
        /** Code-39 detection. */
        NativeBarcodeValueTypeISBN = 3,
        /** Code-93 detection. */
        NativeBarcodeValueTypePhone = 4,
        /** Codabar detection. */
        NativeBarcodeValueTypeProduct = 5,
        /** Data Matrix detection. */
        NativeBarcodeValueTypeSMS = 6,
        /** EAN-13 detection. */
        NativeBarcodeValueTypeText = 7,
        /** EAN-8 detection. */
        NativeBarcodeValueTypeURL = 8,
        /** ITF detection. */
        NativeBarcodeValueTypeWiFi = 9,
        /** QR Code detection. */
        NativeBarcodeValueTypeGeographicCoordinates = 10,
        /** UPC-A detection. */
        NativeBarcodeValueTypeCalendarEvent = 11,
        /** UPC-E detection. */
        NativeBarcodeValueTypeDriversLicense = 12
    };


    internal enum NativeBarcodeAddressType
    {
        NativeBarcodeAddressTypeUnknown = 0,
        NativeBarcodeAddressTypeWork = 1,
        NativeBarcodeAddressTypeHome = 2
    }

    internal enum NativeBarcodeWifiEncryptionType
    {
        NativeBarcodeWiFiEncryptionTypeUnknown = 0,
        NativeBarcodeWiFiEncryptionTypeOpen = 1,
        NativeBarcodeWiFiEncryptionTypeWPA = 2,
        NativeBarcodeWiFiEncryptionTypeWEP = 3
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeAddress
    {
        public NativeArray AddressLines;
        public NativeBarcodeAddressType AddressType;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeEmail
    {
        public IntPtr Address;
        public IntPtr Body;
        public IntPtr Subject;
        public NativeBarcodeEmailType EmailType;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodePhone
    {
        public IntPtr Number;
        public NativeBarcodePhoneType PhoneType;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeSMS
    {
        public IntPtr Message;
        public IntPtr PhoneNumber;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeURLBookmark
    {
        public IntPtr Title;
        public IntPtr Url;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeWifi
    {
        public IntPtr Ssid;
        public IntPtr Password;
        public NativeBarcodeWifiEncryptionType EncryptionType;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeGeoPoint
    {
        public double Latitude;
        public double Longitude;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodePersonName
    {
        public IntPtr FormattedName;
        public IntPtr First;
        public IntPtr Last;
        public IntPtr Middle;
        public IntPtr Prefix;
        public IntPtr Pronunciation;
        public IntPtr Suffix;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeContactInfo
    {
        public NativeArray Addresses;
        public NativeArray Emails;
        public NativeArray Phones;
        public NativeArray Urls;
        public NativeBarcodePersonName Name;
        public IntPtr JobTitle;
        public IntPtr Organisation;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeCalendarEvent
    {
        public IntPtr EventDescription;
        public IntPtr Location;
        public IntPtr Organizer;
        public IntPtr Status;
        public IntPtr Summary;
        public long StartTimestamp;
        public long EndTimestamp;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcodeDrivingLicense
    {
        public IntPtr FirstName;
        public IntPtr MiddleName;
        public IntPtr LastName;
        public IntPtr Gender;
        public IntPtr AddressCity;
        public IntPtr AddressState;
        public IntPtr AddressStreet;
        public IntPtr AddressZip;
        public long BirthTimestamp;
        public IntPtr DocumentType;
        public IntPtr LicenseNumber;
        public long ExpiryTimestamp;
        public long IssueTimestamp;
        public IntPtr IssuingCountry;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeBarcode
    {
        public UnityRect Frame;
        public IntPtr RawValue;
        public IntPtr RawBytes;
        public IntPtr DisplayValue;
        public NativeBarcodeFormat Format;
        public NativeBarcodeValueType ValueType;
        public NativeBarcodeEmail Email;
        public NativeBarcodePhone Phone;
        public NativeBarcodeSMS Sms;
        public NativeBarcodeURLBookmark UrlBookmark;
        public NativeBarcodeWifi Wifi;
        public NativeBarcodeGeoPoint GeoPoint;
        public NativeBarcodeContactInfo ContactInfo;
        public NativeBarcodeCalendarEvent CalendarEvent;
        public NativeBarcodeDrivingLicense DrivingLicense;
    };
}
#endif