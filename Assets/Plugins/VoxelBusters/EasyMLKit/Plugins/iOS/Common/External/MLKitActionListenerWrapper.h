//
//  MLKitActionListenerWrapper.h
//  UnityFramework
//
//  Created by Ayyappa J on 05/04/23.
//

#import <Foundation/Foundation.h>
#import "IMLKitActionListener.h"
#import "NPKit.h"

NS_ASSUME_NONNULL_BEGIN


typedef void (*ActionSuccessNativeCallback)(void* tagPtr);
typedef void (*ActionFailedNativeCallback)(void* tagPtr, const NPError error);


@interface MLKitActionListenerWrapper : NSObject<IMLKitActionListener>
@property (nonatomic, assign) ActionSuccessNativeCallback onSuccessCallback;
@property (nonatomic, assign) ActionFailedNativeCallback onFailureCallback;

-(id) initWithTag:(void*) tag;
@end

NS_ASSUME_NONNULL_END
