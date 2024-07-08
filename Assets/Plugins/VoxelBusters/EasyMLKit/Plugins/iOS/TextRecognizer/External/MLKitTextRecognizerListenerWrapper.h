//
//  IUnityMLKitTextRecognizerListener.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import <Foundation/Foundation.h>
#import "IMLKitTextRecognizerListener.h"

NS_ASSUME_NONNULL_BEGIN
typedef void (*TextRecognizerPrepareSuccessNativeCallback)(void* tagPtr);
typedef void (*TextRecognizerPrepareFailedNativeCallback)(void* tagPtr, NPError error);
typedef void (*TextRecognizerSuccessNativeCallback)(void* tagPtr, MLKitText text, NPSize inputSize, float inputRotation);
typedef void (*TextRecognizerFailedNativeCallback)(void* tagPtr, NPError error);

@interface MLKitTextRecognizerListenerWrapper : NSObject<IMLKitTextRecognizerListener>
@property (nonatomic, assign) TextRecognizerPrepareSuccessNativeCallback prepareSuccessCallback;
@property (nonatomic, assign) TextRecognizerPrepareFailedNativeCallback prepareFailedCallback;
@property (nonatomic, assign) TextRecognizerSuccessNativeCallback scanSuccessCallback;
@property (nonatomic, assign) TextRecognizerFailedNativeCallback scanFailedCallback;

-(id) initWithTag:(void*) tag;
@end


NS_ASSUME_NONNULL_END
