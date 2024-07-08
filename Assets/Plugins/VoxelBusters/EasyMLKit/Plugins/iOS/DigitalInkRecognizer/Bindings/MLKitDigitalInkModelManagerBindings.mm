//
//  MLKitDigitalInkModelManagerBindings.m
//  UnityFramework
//
//  Created by Ayyappa J on 06/04/23.
//

#import "MLKitDigitalInkModelManagerBindings.h"
#import "MLKitDigitalInkModelManager.h"
#import "MLKitActionListenerWrapper.h"
#import "MLKitCommon.h"

@implementation MLKitDigitalInkModelManagerBindings


NPBINDING DONTSTRIP void* MLKit_DigitalInkModelManager_Init()
{
    return  (__bridge_retained void*)[[MLKitDigitalInkModelManager alloc] init];
}

NPBINDING DONTSTRIP BOOL MLKit_DigitalInkModelManager_IsModelAvailable(void* modelManager, const MLKitDigitalInkModelIdentifier* modelIdentifier)
{
    MLKitDigitalInkModelManager*     manager  = (__bridge MLKitDigitalInkModelManager*) modelManager;
    NSString *identifier =  NPCreateNSStringFromCString(modelIdentifier->identifier);
    MLKDigitalInkRecognitionModelIdentifier *nativeModelIdentifier = [MLKDigitalInkRecognitionModelIdentifier modelIdentifierForLanguageTag:identifier];
    return [manager isModelAvailable:nativeModelIdentifier];
}

NPBINDING DONTSTRIP void MLKit_DigitalInkModelManager_DownloadModel(void* modelManager, const MLKitDigitalInkModelIdentifier* modelIdentifier, const MLKitNativeCallbackData0* successNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback)
{
    MLKitDigitalInkModelManager*     manager  = (__bridge MLKitDigitalInkModelManager*) modelManager;
    NSString *identifier =  NPCreateNSStringFromCString(modelIdentifier->identifier);
    MLKDigitalInkRecognitionModelIdentifier *nativeModelIdentifier = [MLKDigitalInkRecognitionModelIdentifier modelIdentifierForLanguageTag:identifier];
    
    
    MLKitNativeCallbackData0 success = getStackAllocatedStruct(successNativeCallback);
    MLKitNativeCallbackData1<NPError> failed = getStackAllocatedStruct(failedNativeCallback);
    
    
    [manager downloadModel:nativeModelIdentifier completion:^(NSError * _Nullable error) {
        if(success.tag != 0)
        {
            if(error == nil)
            {
                success.callback(success.tag);
            }
            else
            {
                failed.callback(failed.tag, NPCreateError(0, [error localizedDescription]));
            }
        }
        
    }];
}

NPBINDING DONTSTRIP void MLKit_DigitalInkModelManager_DeleteModel(void* modelManager, const MLKitDigitalInkModelIdentifier* modelIdentifier, const MLKitNativeCallbackData0* successNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback)
{
    MLKitDigitalInkModelManager*     manager  = (__bridge MLKitDigitalInkModelManager*) modelManager;
    NSString *identifier =  NPCreateNSStringFromCString(modelIdentifier->identifier);
    MLKDigitalInkRecognitionModelIdentifier *nativeModelIdentifier = [MLKDigitalInkRecognitionModelIdentifier modelIdentifierForLanguageTag:identifier];
        

    MLKitNativeCallbackData0 success = getStackAllocatedStruct(successNativeCallback);
    MLKitNativeCallbackData1<NPError> failed = getStackAllocatedStruct(failedNativeCallback);
        
    [manager deleteModel:nativeModelIdentifier completion:^(NSError * _Nullable error) {
        if(success.tag != 0)
        {
            if(error == nil)
            {
                success.callback(success.tag);
            }
            else
            {
                failed.callback(failed.tag, NPCreateError(0, [error localizedDescription]));
            }
        }
        
    }];
}



@end
