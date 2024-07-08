//
//  IMLKitTextRecognizerImplementation.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/06/22.
//

#import <Foundation/Foundation.h>
#import "MLKitTextRecognizerScanOptions.h"
#import "IMLKitTextRecognizerListener.h"
#import "IVisionInputSourceFeed.h"

NS_ASSUME_NONNULL_BEGIN

@protocol IMLKitTextRecognizerImplementation <NSObject>
//Takes the listener and reports the callbacks through it
-(void) setListener:(id<IMLKitTextRecognizerListener>) listener;

//Prepare the scanner by considering the passed options
-(void) prepare:(id<IVisionInputSourceFeed>) inputSourceFeed withOptions:(MLKitTextRecognizerScanOptions*) scanOptions;

//Start processing for scanning Barcodes
-(void) process;

//Shutdown any resources used earlier for scanning
-(void) close;

@end

NS_ASSUME_NONNULL_END
