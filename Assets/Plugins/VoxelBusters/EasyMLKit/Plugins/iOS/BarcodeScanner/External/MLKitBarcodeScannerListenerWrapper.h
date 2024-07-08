//
//  IUnityMLKitBarcodeScannerListener.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import <Foundation/Foundation.h>
#import "IMLKitBarcodeScannerListener.h"

NS_ASSUME_NONNULL_BEGIN
typedef void (*BarcodeScannerPrepareSuccessNativeCallback)(void* tagPtr);
typedef void (*BarcodeScannerPrepareFailedNativeCallback)(void* tagPtr, NPError error);
typedef void (*BarcodeScannerScanSuccessNativeCallback)(void* tagPtr, NPArrayProxy barcodes, NPSize inputSize, float inputRotation);
typedef void (*BarcodeScannerScanFailedNativeCallback)(void* tagPtr, NPError error);

@interface MLKitBarcodeScannerListenerWrapper : NSObject<IMLKitBarcodeScannerListener>
@property (nonatomic, assign) BarcodeScannerPrepareSuccessNativeCallback prepareSuccessCallback;
@property (nonatomic, assign) BarcodeScannerPrepareFailedNativeCallback prepareFailedCallback;
@property (nonatomic, assign) BarcodeScannerScanSuccessNativeCallback scanSuccessCallback;
@property (nonatomic, assign) BarcodeScannerScanFailedNativeCallback scanFailedCallback;

-(id) initWithTag:(void*) tag;
@end


NS_ASSUME_NONNULL_END
