//
//  ARKitCameraInputSourceBindings.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 03/06/22.
//

#import <Foundation/Foundation.h>
#import "NPKit.h"
NS_ASSUME_NONNULL_BEGIN

typedef struct ARFoundationNativeSessionPtr
{
    int version;
    void* session;
} ARFoundationNativeSessionPtr;

NPBINDING DONTSTRIP void* MLKit_CreateARKitCameraInputSourceFeed_InitWithARSession(ARFoundationNativeSessionPtr* arSessionStructPtr);
NPBINDING DONTSTRIP void MLKit_ARKitCameraInputSourceFeed_OnCameraFrameReceived(void* inputSourcePtr);


NS_ASSUME_NONNULL_END
