//
//  MLKitTextRecognizerScanOptionsBindings.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "MLKitTextRecognizerScanOptionsBindings.h"
#import "MLKitTextRecognizerScanOptions.h"
#import "NPBindingHelper.h"

NPBINDING DONTSTRIP void* MLKit_TextRecognizerScanOptions_Init()
{
    return (__bridge_retained void*)[[MLKitTextRecognizerScanOptions alloc] init];
}

NPBINDING DONTSTRIP void* MLKit_TextRecognizerScanOptions_GetLanguage(void* scanOptionsPtr)
{
    MLKitTextRecognizerScanOptions*     scanOptions  = (__bridge MLKitTextRecognizerScanOptions*)scanOptionsPtr;
    return (void*)[scanOptions.language UTF8String];
}

NPBINDING DONTSTRIP void MLKit_TextRecognizerScanOptions_SetLanguage(void* scanOptionsPtr, const char* inputLanguage)
{
    MLKitTextRecognizerScanOptions*     scanOptions  = (__bridge MLKitTextRecognizerScanOptions*)scanOptionsPtr;
    scanOptions.language = NPCreateNSStringFromCString(inputLanguage);
}
