//
//  TextRecognizer.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import <Foundation/Foundation.h>
#import "IMLKitTextRecognizerImplementation.h"
#import "IVisionInputSourceFeed.h"

NS_ASSUME_NONNULL_BEGIN

@interface MLKitTextRecognizer : NSObject<IMLKitTextRecognizerImplementation, IVisionInputSourceFeedDelegate>
- (id)init;
@end

NS_ASSUME_NONNULL_END
