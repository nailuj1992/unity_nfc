//
//  MLKitBarcode.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 31/05/22.
//
#import "MLKitBarcode.h"
#import <MLKitBarcodeScanning/MLKBarcode.h>

NPBINDING DONTSTRIP MLKitBarcodeEmail getEmail(MLKBarcodeEmail* source);
NPBINDING DONTSTRIP long getTimestampFromDateString(NSString* dateStr);
NPBINDING DONTSTRIP MLKitBarcodeEmail getEmail(MLKBarcodeEmail *source);
NPBINDING DONTSTRIP MLKitBarcodePhone getPhone(MLKBarcodePhone *source);
NPBINDING DONTSTRIP MLKitBarcodeSMS getSms(MLKBarcodeSMS *source);
NPBINDING DONTSTRIP MLKitBarcodeURLBookmark getUrlBookmark(MLKBarcodeURLBookmark *source);
NPBINDING DONTSTRIP MLKitBarcodeWifi getWifi(MLKBarcodeWiFi *source);
NPBINDING DONTSTRIP MLKitBarcodeGeoPoint getGeoPoint(MLKBarcodeGeoPoint *source);
NPBINDING DONTSTRIP MLKitBarcodeContactInfo* getContactInfo(MLKBarcodeContactInfo *source);
NPBINDING DONTSTRIP MLKitBarcodeCalendarEvent getCalendarEvent(MLKBarcodeCalendarEvent *source);
NPBINDING DONTSTRIP MLKitBarcodeDrivingLicense getDrivingLicense(MLKBarcodeDriverLicense *source);
NPBINDING DONTSTRIP MLKitBarcodePersonName getPersonName(MLKBarcodePersonName *source);
NPBINDING DONTSTRIP NSArray* getPhones(NSArray<MLKBarcodePhone*>* source);
NPBINDING DONTSTRIP NSArray* getEmails(NSArray<MLKBarcodeEmail*>* source);
NPBINDING DONTSTRIP NSArray* getAddresses(NSArray<MLKBarcodeAddress*>* source);

NPBINDING DONTSTRIP void convertToMLKitBarcode(MLKBarcode* source, MLKitBarcode* destination)
{
    memset(destination, 0, sizeof(MLKitBarcode));
    destination->frame = getRect(source.frame);
    destination->rawValue = (void*)[source.rawValue UTF8String];
    destination->rawBytes = (void*)[[source.rawData base64EncodedStringWithOptions:0] UTF8String];
    destination->displayValue = (void*) [source.displayValue UTF8String];
    destination->format = (MLKitBarcodeFormat)source.format;
    destination->valueType = (MLKitBarcodeValueType)source.valueType;
    destination->email = getEmail(source.email);
    destination->phone = getPhone(source.phone);
    destination->sms = getSms(source.sms);
    destination->urlBookmark = getUrlBookmark(source.URL);
    destination->wifi = getWifi(source.wifi);
    destination->geoPoint = getGeoPoint(source.geoPoint);
    destination->contactInfo = *(getContactInfo(source.contactInfo));
    destination->calendarEvent = getCalendarEvent(source.calendarEvent);
    destination->drivingLicense = getDrivingLicense(source.driverLicense);
}

NPBINDING DONTSTRIP CGSize getSizeAfterRotation(CGSize input, float rotation)
{
    CGSize rawTransformedDimensions = CGSizeApplyAffineTransform(input, CGAffineTransformRotate(CGAffineTransformIdentity, rotation));
    return CGSizeMake(ABS(rawTransformedDimensions.width), ABS(rawTransformedDimensions.height));
}

NPBINDING DONTSTRIP float getRequiredScaleForAspectFill(CGSize source, CGSize destination)
{
    float displayAspect  = destination.width/destination.height;
    float newInputAspect = source.width/source.height;
        
    CGSize fillSize; //Is the size required to aspect fill the input to display
    
    if(displayAspect > newInputAspect) //This means display is more wider than the input. If we match width of input to display width, as input is more in height, it fills the screen. If we try to match height here, as input's aspect is small, it won't fill the display width.
    {
        fillSize.width = destination.width;
        fillSize.height = fillSize.width/newInputAspect;
    }
    else //Match height. This means display is more narrower than input. If we match height of the display, as input is already higher in aspect (wider), it fills the displays width.
    {
        fillSize.height = destination.height;
        fillSize.width = fillSize.height * newInputAspect;
    }
    
    
    float requiredScale = fillSize.width/source.width;
    return requiredScale;
}

NPBINDING DONTSTRIP MLKitBarcodeEmail getEmail(MLKBarcodeEmail *source)
{
    MLKitBarcodeEmail email;
    email.address = (void*)[source.address UTF8String];
    email.body = (void*)[source.body UTF8String];
    email.emailType = (MLKitBarcodeEmailType)source.type;
    email.subject = (void*)[source.subject UTF8String];
    return email;
}


NPBINDING DONTSTRIP MLKitBarcodePhone getPhone(MLKBarcodePhone *source)
{
    MLKitBarcodePhone phone;
    phone.number = (void*) [source.number UTF8String];
    phone.phoneType = (MLKitBarcodePhoneType) source.type;
    return phone;
}


NPBINDING DONTSTRIP MLKitBarcodeSMS getSms(MLKBarcodeSMS *source)
{
    MLKitBarcodeSMS sms;
    sms.message = (void*) [source.message UTF8String];
    return sms;
}

NPBINDING DONTSTRIP MLKitBarcodeURLBookmark getUrlBookmark(MLKBarcodeURLBookmark *source)
{
    MLKitBarcodeURLBookmark urlBookmark;
    urlBookmark.title = (void*) [source.title UTF8String];
    urlBookmark.url = (void*) [source.url UTF8String];
    return urlBookmark;
}


NPBINDING DONTSTRIP MLKitBarcodeWifi getWifi(MLKBarcodeWiFi *source)
{
    MLKitBarcodeWifi wifi;
    wifi.password = (void*) [source.password UTF8String];
    wifi.ssid = (void*) [source.ssid UTF8String];
    return wifi;
}


NPBINDING DONTSTRIP MLKitBarcodeGeoPoint getGeoPoint(MLKBarcodeGeoPoint *source)
{
    MLKitBarcodeGeoPoint geoPoint;
    geoPoint.latitude = source.latitude;
    geoPoint.longitude = source.longitude;
    
    return geoPoint;
}

NPBINDING DONTSTRIP MLKitBarcodeContactInfo* getContactInfo(MLKBarcodeContactInfo *source)
{
 MLKitBarcodeContactInfo *contactInfo = (MLKitBarcodeContactInfo*)malloc(sizeof(MLKitBarcodeContactInfo));
 memset(contactInfo, 0, sizeof(MLKitBarcodeContactInfo));
 contactInfo->addresses = *(NPCreateNativeArrayFromNSArray(getAddresses(source.addresses))); //TODO
 contactInfo->emails = *(NPCreateNativeArrayFromNSArray(getEmails(source.emails)));
 contactInfo->phones = *(NPCreateNativeArrayFromNSArray(getPhones(source.phones)));
 contactInfo->urls = *(NPCreateArrayOfCString(source.urls));
 
 contactInfo->name = getPersonName(source.name);
 contactInfo->jobTitle = (void*) [source.jobTitle UTF8String];
 contactInfo->organisation = (void*) [source.organization UTF8String];
 return contactInfo;
}

NPBINDING DONTSTRIP NSArray* getAddresses(NSArray<MLKBarcodeAddress*>* source)
{
    NSMutableArray *addresses = [NSMutableArray array];
    for (MLKBarcodeAddress* each in source) {
        MLKitBarcodeAddress *address = (MLKitBarcodeAddress*)malloc(sizeof(MLKitBarcodeAddress));
        memset(address, 0, sizeof(MLKitBarcodeAddress));
        address->addressLines = *(NPCreateArrayOfCString(each.addressLines));
        address->addressType = (MLKitBarcodeAddressType)each.type;
        NSValue *value = [NSValue valueWithPointer:address];
        [addresses addObject:value];
    }
    
    return addresses;
}

NPBINDING DONTSTRIP NSArray* getEmails(NSArray<MLKBarcodeEmail*>* source)
{
    NSMutableArray *emails = [NSMutableArray array];
    for (MLKBarcodeEmail* each in source) {
        MLKitBarcodeEmail *email = (MLKitBarcodeEmail*)malloc(sizeof(MLKitBarcodeEmail));//Release this
        memset(email, 0, sizeof(MLKitBarcodeEmail));
        email->address = NPCreateCStringFromNSString(each.address);
        email->body = NPCreateCStringFromNSString(each.body);
        email->subject = NPCreateCStringFromNSString(each.subject);
        email->emailType = (MLKitBarcodeEmailType)each.type;

        NSValue *value = [NSValue valueWithPointer:email];
        [emails addObject:value];
    }
    
    return emails;
}

NPBINDING DONTSTRIP NSArray* getPhones(NSArray<MLKBarcodePhone*>* source)
{
    NSMutableArray *phones = [NSMutableArray array];
    for (MLKBarcodePhone* each in source) {
        MLKitBarcodePhone *phone = (MLKitBarcodePhone*)malloc(sizeof(MLKitBarcodePhone));//Release this
        phone->number = NPCreateCStringFromNSString(each.number);
        phone->phoneType = (MLKitBarcodePhoneType)each.type;

        NSValue *value = [NSValue valueWithPointer:phone];
        [phones addObject:value];
    }
    
    return phones;
}

NPBINDING DONTSTRIP MLKitBarcodePersonName getPersonName(MLKBarcodePersonName *source)
{
    MLKitBarcodePersonName person;
    person.prefix = NPCreateCStringFromNSString(source.prefix);
    person.first =  NPCreateCStringFromNSString(source.first);
    person.formattedName =  NPCreateCStringFromNSString(source.formattedName);
    person.last =  NPCreateCStringFromNSString(source.last);
    person.middle =  NPCreateCStringFromNSString(source.middle);
    person.pronunciation =  NPCreateCStringFromNSString(source.pronunciation);
    person.suffix =  NPCreateCStringFromNSString(source.suffix);
    return person;
}


NPBINDING DONTSTRIP MLKitBarcodeCalendarEvent getCalendarEvent(MLKBarcodeCalendarEvent *source)
{
    MLKitBarcodeCalendarEvent event;
    event.eventDescription = (void*) [source.eventDescription UTF8String];
    event.location = (void*) [source.location UTF8String];
    event.organizer = (void*) [source.organizer UTF8String];
    event.status = (void*) [source.status UTF8String];
    event.summary = (void*) [source.summary UTF8String];
    
    event.startTimestamp = [source.start timeIntervalSince1970];
    event.endTimestamp = [source.end timeIntervalSince1970];
    return event;
}

NPBINDING DONTSTRIP MLKitBarcodeDrivingLicense getDrivingLicense(MLKBarcodeDriverLicense *source)
{
    MLKitBarcodeDrivingLicense license;
    license.addressCity = (void*)[source.addressCity UTF8String];
    license.addressState = (void*) [source.addressState UTF8String];
    license.addressStreet = (void*) [source.addressStreet UTF8String];
    license.addressZip = (void*) [source.addressZip UTF8String];
    license.birthTimestamp = getTimestampFromDateString(source.birthDate);
    license.documentType = (void*) [source.documentType UTF8String];
    license.licenseNumber = (void*) [source.licenseNumber UTF8String];
    license.expiryTimestamp = getTimestampFromDateString(source.expiryDate);
    license.issueTimestamp = getTimestampFromDateString(source.issuingDate);
    license.issuingCountry = (void*) [source.issuingCountry UTF8String];
    return license;
}

NPBINDING DONTSTRIP long getTimestampFromDateString(NSString* dateStr)
{
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"dd-MM-yyyy"]; //@"yyyy-MM-dd HH:mm:ss zzz"
    NSDate *date = [dateFormatter dateFromString:dateStr];
    return [date timeIntervalSince1970];
}

