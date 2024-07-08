//
//  MLKitDigitalInkRecognizerExternalBindings.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "NPKit.h"
#import "MLKitDigitalInkRecognizerTypes.h"

NS_ASSUME_NONNULL_BEGIN

NPBINDING DONTSTRIP void* MLKit_DigitalInkRecognizer_Init();
NPBINDING DONTSTRIP void MLKit_DigitalInkRecognizer_Prepare(void* digitalInkRecognizerPtr, const MLKitDigitalInkDrawing* input, const MLKitDigitalInkRecognizerOptions* options, const MLKitNativeCallbackData0* successNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback);
NPBINDING DONTSTRIP void MLKit_DigitalInkRecognizer_Process(void* digitalInkRecognizerPtr, const MLKitNativeCallbackData1<NPArrayProxy>* onUpdateNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback);
NPBINDING DONTSTRIP void MLKit_DigitalInkRecognizer_Close(void* digitalInkRecognizerPtr);
NPBINDING DONTSTRIP void* MLKit_DigitalInkRecognizer_GetModelManager(void* digitalInkRecognizerPtr);

NS_ASSUME_NONNULL_END
