//
//  ImageInputSourceFeed.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//

#import <Foundation/Foundation.h>
#import "IVisionInputSourceFeed.h"

NS_ASSUME_NONNULL_BEGIN

@interface ImageInputSourceFeed : NSObject<IVisionInputSourceFeed>
-(id) initWithImage:(UIImage*) image;
@end

NS_ASSUME_NONNULL_END
