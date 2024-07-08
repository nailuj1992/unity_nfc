//
//  MLKitBarcodeScanOptionsBindings.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "MLKitBarcodeScanOptionsBindings.h"
#import "MLKitBarcodeScanOptions.h"


NPBINDING DONTSTRIP void* MLKit_BarcodeScanOptions_Init()
{
    return (__bridge_retained void*)[[MLKitBarcodeScanOptions alloc] init];
}

NPBINDING DONTSTRIP int MLKit_BarcodeScanOptions_GetAllowedFormats(void* scanOptionsPtr)
{
    MLKitBarcodeScanOptions*     scanOptions  = (__bridge MLKitBarcodeScanOptions*)scanOptionsPtr;
    return scanOptions.allowedFormats;
}

NPBINDING DONTSTRIP void MLKit_BarcodeScanOptions_SetAllowedFormats(void* scanOptionsPtr, int formats)
{
    MLKitBarcodeScanOptions*     scanOptions  = (__bridge MLKitBarcodeScanOptions*)scanOptionsPtr;
    scanOptions.allowedFormats = formats;
}
