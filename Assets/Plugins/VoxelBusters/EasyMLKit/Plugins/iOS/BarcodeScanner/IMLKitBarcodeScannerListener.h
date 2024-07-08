//
//  IBarcodeScannerListener.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//

#import <Foundation/Foundation.h>
#import <MLKitBarcodeScanning/MLKBarcode.h>
#import "MLKitBarcode.h"
#import "MLKitBarcodeScanResult.h"

NS_ASSUME_NONNULL_BEGIN

@protocol IMLKitBarcodeScannerListener <NSObject>
-(void) onPrepareSuccess;
-(void) onPrepareFailed:(NSError*) error;
-(void) onScanSuccess:(MLKitBarcodeScanResult*) result;
-(void) onScanFailed:(NSError*) error;
@end

NS_ASSUME_NONNULL_END
