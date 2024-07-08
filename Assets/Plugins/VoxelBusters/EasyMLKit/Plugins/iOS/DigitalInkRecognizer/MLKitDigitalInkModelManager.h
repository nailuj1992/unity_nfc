//
//  MLKitDigitalInkModelManager.h
//  UnityFramework
//
//  Created by Ayyappa J on 05/04/23.
//

#import <Foundation/Foundation.h>
#import <MLKitDigitalInkRecognition/MLKDigitalInkRecognitionModelIdentifier.h>
#import "MLKitCommon.h"

NS_ASSUME_NONNULL_BEGIN

@interface MLKitDigitalInkModelManager : NSObject
-(BOOL) isModelAvailable:(MLKDigitalInkRecognitionModelIdentifier*) modelIdentifier;
-(void) downloadModel:(MLKDigitalInkRecognitionModelIdentifier*) modelIdentifier completion:(CompletionBlock) completionBlock;
-(void) deleteModel:(MLKDigitalInkRecognitionModelIdentifier*) modelIdentifier completion:(CompletionBlock) completionBlock;

@end

NS_ASSUME_NONNULL_END
