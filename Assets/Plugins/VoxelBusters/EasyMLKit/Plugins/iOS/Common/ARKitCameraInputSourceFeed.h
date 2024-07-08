//
//  ARKitCameraInputSourceFeed.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//

#import <Foundation/Foundation.h>
#import "IVisionInputSourceFeed.h"
#import <ARKit/ARSession.h>

NS_ASSUME_NONNULL_BEGIN

@interface ARKitCameraInputSourceFeed : NSObject<IVisionInputSourceFeed>
-(id) initWithARSession:(ARSession*) arSession;
-(void) onCameraFrameReceived;
@end

NS_ASSUME_NONNULL_END
