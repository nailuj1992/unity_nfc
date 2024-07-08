//
//  MLKitBarcodeScanOptionsBindings.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import <Foundation/Foundation.h>
#import "NPKit.h"
NS_ASSUME_NONNULL_BEGIN

NPBINDING DONTSTRIP void* MLKit_TextRecognizerScanOptions_Init();
NPBINDING DONTSTRIP void* MLKit_TextRecognizerScanOptions_GetLanguage(void* options);
NPBINDING DONTSTRIP void MLKit_TextRecognizerScanOptions_SetLanguage(void* scanOptionsPtr, const char* inputLanguage);

NS_ASSUME_NONNULL_END
