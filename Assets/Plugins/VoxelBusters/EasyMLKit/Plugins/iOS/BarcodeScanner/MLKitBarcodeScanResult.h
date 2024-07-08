//
//  MLKitBarcodeScanResult.h
//  UnityFramework
//
//  Created by Ayyappa J on 10/06/22.
//

#import <Foundation/Foundation.h>
#import <MLKitBarcodeScanning/MLKBarcode.h>

NS_ASSUME_NONNULL_BEGIN

@interface MLKitBarcodeScanResult : NSObject
@property (nonatomic, strong) NSArray<MLKBarcode*>* barcodes;
@property (nonatomic) float inputRotation;
@property (nonatomic) CGSize inputSize;
@end
NS_ASSUME_NONNULL_END
