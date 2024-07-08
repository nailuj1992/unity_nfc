//
//  Barcode.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 30/05/22.
//

#ifndef MLKitBarcode_h
#define MLKitBarcode_h
#define CHAR_STRING void*


#import <MLKitBarcodeScanning/MLKBarcode.h>
#import "NPKit.h"
#import "MLKitCommon.h"

// custom datatypes

typedef enum
{
    MLKitBarcodeAddressTypeUnknown = 0,
    MLKitBarcodeAddressTypeWork = 1,
    MLKitBarcodeAddressTypeHome = 2
} MLKitBarcodeAddressType;


typedef struct
{
    NPArray addressLines;
    MLKitBarcodeAddressType addressType;
} MLKitBarcodeAddress;

typedef enum
{
    MLKitBarcodeEmailTypeUnknown = 0,
    MLKitBarcodeEmailTypeWork = 1,
    MLKitBarcodeEmailTypeHome = 2
} MLKitBarcodeEmailType;

typedef struct
{
    CHAR_STRING address;
    CHAR_STRING body;
    CHAR_STRING subject;
    MLKitBarcodeEmailType emailType;
} MLKitBarcodeEmail;


typedef enum
{
    MLKitBarcodePhoneTypeUnknown = 0,
    MLKitBarcodePhoneTypeWork = 1,
    MLKitBarcodePhoneTypeHome = 2,
    MLKitBarcodePhoneTypeFax = 3,
    MLKitBarcodePhoneTypeMobile = 4
} MLKitBarcodePhoneType;


typedef struct
{
    CHAR_STRING number;
    MLKitBarcodePhoneType phoneType;
} MLKitBarcodePhone;


typedef struct
{
    CHAR_STRING message;
    CHAR_STRING phoneNumber;
} MLKitBarcodeSMS;

typedef struct
{
    CHAR_STRING title;
    CHAR_STRING url;
} MLKitBarcodeURLBookmark;

typedef enum
{
    MLKitBarcodeWiFiEncryptionTypeUnknown = 0,
    MLKitBarcodeWiFiEncryptionTypeOpen = 1,
    MLKitBarcodeWiFiEncryptionTypeWPA = 2,
    MLKitBarcodeWiFiEncryptionTypeWEP = 3
} MLKitBarcodeWifiEncryptionType;


typedef struct
{
    CHAR_STRING ssid;
    CHAR_STRING password;
    MLKBarcodeWiFiEncryptionType encryptionType;
} MLKitBarcodeWifi;

typedef struct
{
    double latitude;
    double longitude;
} MLKitBarcodeGeoPoint;

typedef struct
{
    CHAR_STRING formattedName;
    CHAR_STRING first;
    CHAR_STRING last;
    CHAR_STRING middle;
    CHAR_STRING prefix;
    CHAR_STRING pronunciation;
    CHAR_STRING suffix;
} MLKitBarcodePersonName;

typedef struct
{
    NPArray addresses;
    NPArray emails;
    NPArray phones;
    NPArray urls;
    MLKitBarcodePersonName name;
    CHAR_STRING jobTitle;
    CHAR_STRING organisation;
} MLKitBarcodeContactInfo;

typedef struct
{
    CHAR_STRING eventDescription;
    CHAR_STRING location;
    CHAR_STRING organizer;
    CHAR_STRING status;
    CHAR_STRING summary;
    long startTimestamp;
    long endTimestamp;
} MLKitBarcodeCalendarEvent;

typedef struct
{
    CHAR_STRING firstName;
    CHAR_STRING middleName;
    CHAR_STRING lastName;
    CHAR_STRING gender;
    CHAR_STRING addressCity;
    CHAR_STRING addressState;
    CHAR_STRING addressStreet;
    CHAR_STRING addressZip;
    long birthTimestamp;
    CHAR_STRING documentType;
    CHAR_STRING licenseNumber;
    long expiryTimestamp;
    long issueTimestamp;
    CHAR_STRING issuingCountry;
} MLKitBarcodeDrivingLicense;

typedef enum
{
    /** Unknown format. */
    MLKitBarcodeFormatUnknown = 0,
    /** All format. */
    MLKitBarcodeFormatAll = 1,
    /** Code-128 detection. */
    MLKitBarcodeFormatCode128 = 2,
    /** Code-39 detection. */
    MLKitBarcodeFormatCode39 = 3,
    /** Code-93 detection. */
    MLKitBarcodeFormatCode93 = 4,
    /** Codabar detection. */
    MLKitBarcodeFormatCodaBar = 5,
    /** Data Matrix detection. */
    MLKitBarcodeFormatDataMatrix = 6,
    /** EAN-13 detection. */
    MLKitBarcodeFormatEAN13 = 7,
    /** EAN-8 detection. */
    MLKitBarcodeFormatEAN8 = 8,
    /** ITF detection. */
    MLKitBarcodeFormatITF = 9,
    /** QR Code detection. */
    MLKitBarcodeFormatQRCode = 10,
    /** UPC-A detection. */
    MLKitBarcodeFormatUPCA = 11,
    /** UPC-E detection. */
    MLKitBarcodeFormatUPCE = 12,
    /** PDF-417 detection. */
    MLKitBarcodeFormatPDF417 = 13,
    /** Aztec code detection. */
    MLKitBarcodeFormatAztec = 14,
} MLKitBarcodeFormat;

typedef enum
{
    /** Unknown format. */
    MLKitBarcodeValueTypeUnknown = 0,
    /** All format. */
    MLKitBarcodeValueTypeContactInfo = 1,
    /** Code-128 detection. */
    MLKitBarcodeValueTypeEmail = 2,
    /** Code-39 detection. */
    MLKitBarcodeValueTypeISBN = 3,
    /** Code-93 detection. */
    MLKitBarcodeValueTypePhone = 4,
    /** Codabar detection. */
    MLKitBarcodeValueTypeProduct = 5,
    /** Data Matrix detection. */
    MLKitBarcodeValueTypeSMS = 6,
    /** EAN-13 detection. */
    MLKitBarcodeValueTypeText = 7,
    /** EAN-8 detection. */
    MLKitBarcodeValueTypeURL = 8,
    /** ITF detection. */
    MLKitBarcodeValueTypeWiFi = 9,
    /** QR Code detection. */
    MLKitBarcodeValueTypeGeographicCoordinates = 10,
    /** UPC-A detection. */
    MLKitBarcodeValueTypeCalendarEvent = 11,
    /** UPC-E detection. */
    MLKitBarcodeValueTypeDriversLicense = 12
} MLKitBarcodeValueType;

struct MLKitBarcode
{
    MLKitRect                   frame;
    CHAR_STRING                 rawValue;
    CHAR_STRING                 rawBytes;
    CHAR_STRING                 displayValue;
    MLKitBarcodeFormat          format;
    MLKitBarcodeValueType       valueType;
    MLKitBarcodeEmail           email;
    MLKitBarcodePhone           phone;
    MLKitBarcodeSMS             sms;
    MLKitBarcodeURLBookmark     urlBookmark;
    MLKitBarcodeWifi            wifi;
    MLKitBarcodeGeoPoint        geoPoint;
    MLKitBarcodeContactInfo     contactInfo;
    MLKitBarcodeCalendarEvent   calendarEvent;
    MLKitBarcodeDrivingLicense  drivingLicense;
};

typedef struct MLKitBarcode MLKitBarcode;

NPBINDING DONTSTRIP void convertToMLKitBarcode(MLKBarcode* source, MLKitBarcode* destination);
#endif /* MLKititBarcode_h */
