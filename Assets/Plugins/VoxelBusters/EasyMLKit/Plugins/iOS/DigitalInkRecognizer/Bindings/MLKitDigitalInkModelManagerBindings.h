//
//  MLKitDigitalInkModelManagerBindings.h
//  UnityFramework
//
//  Created by Ayyappa J on 06/04/23.
//

#import <Foundation/Foundation.h>
#import "MLKitActionListenerWrapper.h"
#import "MLKitDigitalInkRecognizerTypes.h"


NS_ASSUME_NONNULL_BEGIN

@interface MLKitDigitalInkModelManagerBindings : NSObject
NPBINDING DONTSTRIP void* MLKit_DigitalInkModelManager_Init();
NPBINDING DONTSTRIP BOOL MLKit_DigitalInkModelManager_IsModelAvailable(void* modelManager, const MLKitDigitalInkModelIdentifier* modelIdentifier);
NPBINDING DONTSTRIP void MLKit_DigitalInkModelManager_DownloadModel(void* modelManager, const MLKitDigitalInkModelIdentifier* modelIdentifier, const MLKitNativeCallbackData0* successNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback);
NPBINDING DONTSTRIP void MLKit_DigitalInkModelManager_DeleteModel(void* modelManager, const MLKitDigitalInkModelIdentifier* modelIdentifier, const MLKitNativeCallbackData0* successNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback);
@end
NS_ASSUME_NONNULL_END
