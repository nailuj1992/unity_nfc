//
//  MLKitBarcodeScanOptionsBindings.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import <Foundation/Foundation.h>
#import "NPKit.h"
NS_ASSUME_NONNULL_BEGIN

NPBINDING DONTSTRIP void* MLKit_BarcodeScanOptions_Init();
NPBINDING DONTSTRIP int MLKit_BarcodeScanOptions_GetAllowedFormats(void* options);
NPBINDING DONTSTRIP void MLKit_BarcodeScanOptions_SetAllowedFormats(void* scanOptionsPtr, int formats);

NS_ASSUME_NONNULL_END
