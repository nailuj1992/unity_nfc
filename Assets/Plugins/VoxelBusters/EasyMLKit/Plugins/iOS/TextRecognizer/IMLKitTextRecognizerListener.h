//
//  IMLKitTextRecognizerListener.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/06/22.
//

#import <Foundation/Foundation.h>
#import "MLKitText.h"
#import "MLKitTextRecognizerResult.h"

NS_ASSUME_NONNULL_BEGIN

@protocol IMLKitTextRecognizerListener <NSObject>
-(void) onPrepareSuccess;
-(void) onPrepareFailed:(NSError*) error;
-(void) onScanSuccess:(MLKitTextRecognizerResult*) result;
-(void) onScanFailed:(NSError*) error;
@end

NS_ASSUME_NONNULL_END
