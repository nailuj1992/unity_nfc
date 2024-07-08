//
//  IMLKitDigitalInkRecognizerListener.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/06/22.
//

#import <Foundation/Foundation.h>
#import <MLKitDigitalInkRecognition/MLKitDigitalInkRecognition.h>


NS_ASSUME_NONNULL_BEGIN

@protocol IMLKitDigitalInkRecognizerListener <NSObject>
-(void) onPrepareSuccess;
-(void) onPrepareFailed:(NSError*) error;
-(void) onScanSuccess:(NSArray<MLKDigitalInkRecognitionCandidate*>*) candidates;
-(void) onScanFailed:(NSError*) error;
@end

NS_ASSUME_NONNULL_END
