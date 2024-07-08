//
//  DigitalInkRecognizer.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import <Foundation/Foundation.h>
#import <MLKitDigitalInkRecognition/MLKitDigitalInkRecognition.h>
#import "IMLKitDigitalInkRecognizerListener.h"
#import "MLKitDigitalInkModelManager.h"
#import "MLKitCommon.h"

NS_ASSUME_NONNULL_BEGIN

@interface MLKitDigitalInkRecognizer : NSObject

typedef void(^ProcessCompletionBlock)(NSArray<MLKDigitalInkRecognitionCandidate*> *_Nullable candidates, NSError *_Nullable error);

- (void)setListener:(id<IMLKitDigitalInkRecognizerListener>)listener;
-(void) prepare:(MLKInk*) inkInput withOptions: (MLKDigitalInkRecognizerOptions*) options completionBlock: (CompletionBlock) completionBlock;
- (void)process:(ProcessCompletionBlock) completionBlock;
- (void)close;
-(MLKitDigitalInkModelManager*) getModelManager;

@end

NS_ASSUME_NONNULL_END
