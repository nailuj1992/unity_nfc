//
//  ARKitCameraInputSourceBindings.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 03/06/22.
//

#import "ARKitCameraInputSourceBindings.h"
#import "ARKitCameraInputSourceFeed.h"

// C bindings
NPBINDING DONTSTRIP void* MLKit_CreateARKitCameraInputSourceFeed_InitWithARSession(ARFoundationNativeSessionPtr* arSessionStructPtr)
{
    ARSession* session = (__bridge ARSession*)arSessionStructPtr->session;
    return (__bridge_retained void*)[[ARKitCameraInputSourceFeed alloc] initWithARSession:session];
}

NPBINDING DONTSTRIP void MLKit_ARKitCameraInputSourceFeed_OnCameraFrameReceived(void* inputSourcePtr)
{
    ARKitCameraInputSourceFeed*     inputSource     = (__bridge ARKitCameraInputSourceFeed*)inputSourcePtr;
    [inputSource onCameraFrameReceived];
}

