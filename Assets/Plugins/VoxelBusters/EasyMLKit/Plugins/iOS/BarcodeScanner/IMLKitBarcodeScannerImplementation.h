//
//  IBarcodeScannerImplementation.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import <Foundation/Foundation.h>
#import "MLKitBarcodeScanOptions.h"
#import "IMLKitBarcodeScannerListener.h"
#import "IVisionInputSourceFeed.h"

NS_ASSUME_NONNULL_BEGIN

@protocol IMLKitBarcodeScannerImplementation <NSObject>
//Takes the listener and reports the callbacks through it
-(void) setListener:(id<IMLKitBarcodeScannerListener>) listener;

//Prepare the scanner by considering the passed options
-(void) prepare:(id<IVisionInputSourceFeed>) inputSource withOptions:(MLKitBarcodeScanOptions*) scanOptions;

//Start processing for scanning Barcodes
-(void) process;

//Shutdown any resources used earlier for scanning
-(void) close;

@end

NS_ASSUME_NONNULL_END
