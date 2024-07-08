//
//  MLKitTextRecognizerExternalBindings.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "MLKitTextRecognizerListenerWrapper.h"
#import "NPKit.h"

NS_ASSUME_NONNULL_BEGIN

NPBINDING DONTSTRIP void* MLKit_TextRecognizer_Init();
NPBINDING DONTSTRIP void MLKit_TextRecognizer_SetListener( void* textRecognizerPtr,
                              void* listenerTag,
                              TextRecognizerPrepareSuccessNativeCallback prepareSuccessCallback,
                              TextRecognizerPrepareFailedNativeCallback prepareFailedCallback,
                              TextRecognizerSuccessNativeCallback scanSuccessCallback,
                                      TextRecognizerFailedNativeCallback scanFailedCallback);
NPBINDING DONTSTRIP void MLKit_TextRecognizer_Prepare(void* textRecognizerPtr, void* inputSourcePtr, void* textRecognizerScanOptionsPtr);
NPBINDING DONTSTRIP void MLKit_TextRecognizer_Process(void* textRecognizerPtr);
NPBINDING DONTSTRIP void MLKit_TextRecognizer_Close(void* textRecognizerPtr);

NS_ASSUME_NONNULL_END
