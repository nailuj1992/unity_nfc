//
//  MLKitDigitalInkRecognizerExternalBindings.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "MLKitDigitalInkRecognizerBindings.h"
#import "MLKitDigitalInkRecognizer.h"

MLKInk* createInk(const MLKitDigitalInkDrawing* input);
MLKDigitalInkRecognizerOptions* createOptions(const MLKitDigitalInkRecognizerOptions* scanOptions);

// C bindings
NPBINDING DONTSTRIP void* MLKit_DigitalInkRecognizer_Init()
{
    return (__bridge_retained void*)[[MLKitDigitalInkRecognizer alloc] init];
}

NPBINDING DONTSTRIP void MLKit_DigitalInkRecognizer_Prepare(void* digitalInkRecognizerPtr, const MLKitDigitalInkDrawing* input, const MLKitDigitalInkRecognizerOptions* options, const MLKitNativeCallbackData0* successNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback)
{
    MLKitDigitalInkRecognizer*         recognizer  = (__bridge MLKitDigitalInkRecognizer*)digitalInkRecognizerPtr;    
    
    MLKitNativeCallbackData0 success = getStackAllocatedStruct(successNativeCallback);
    MLKitNativeCallbackData1<NPError> failed = getStackAllocatedStruct(failedNativeCallback);
    [recognizer prepare:createInk(input) withOptions: createOptions(options) completionBlock: ^(NSError * _Nullable error) {
        if(error == nil)
        {
            if(success.tag != 0)
            {
                success.callback(success.tag);
            }
        }
        else
        {
            if(failed.tag != 0)
            {
                failed.callback(failed.tag, NPCreateError(0, [error localizedDescription]));
            }
        }
    }];
}

NPBINDING DONTSTRIP void MLKit_DigitalInkRecognizer_Process(void* digitalInkRecognizerPtr, const MLKitNativeCallbackData1<NPArrayProxy>* onUpdateNativeCallback, const MLKitNativeCallbackData1<NPError>* failedNativeCallback)
{
    MLKitDigitalInkRecognizer*         recognizer  = (__bridge MLKitDigitalInkRecognizer*)digitalInkRecognizerPtr;
    
    MLKitNativeCallbackData1<NPArrayProxy> update = getStackAllocatedStruct(onUpdateNativeCallback);
    MLKitNativeCallbackData1<NPError> failed = getStackAllocatedStruct(failedNativeCallback);
    
    [recognizer process: ^(NSArray<MLKDigitalInkRecognitionCandidate*> *_Nullable candidates, NSError *_Nullable error) {
        
        if(error != nil)
        {
            failed.callback(failed.tag, NPCreateError(0, [error localizedDescription]));
            return;
        }
        
        NSMutableArray *values = (candidates == nil || candidates.count == 0) ? nil : [NSMutableArray array];
        for (MLKDigitalInkRecognitionCandidate* each in candidates) {
            MLKitDigitalInkRecognizedValue *recognizedValue = (MLKitDigitalInkRecognizedValue*)malloc(sizeof(MLKitDigitalInkRecognizedValue));
            convertToMLKitDigitalInkRecognizedValue(each, recognizedValue);
            NSValue *value = [NSValue valueWithPointer:recognizedValue];
            [values addObject:value];
        }
        
        NPArray *arr = NPCreateNativeArrayFromNSArray(values);
        
        NPArrayProxy proxy;
        proxy.length = arr->length;
        proxy.ptr = arr->ptr;
        
        if(update.tag != 0)
        {
            update.callback(update.tag, proxy);
        }
    }];
}

NPBINDING DONTSTRIP void MLKit_DigitalInkRecognizer_Close(void* digitalInkRecognizerPtr)
{
    MLKitDigitalInkRecognizer*         recognizer = (__bridge MLKitDigitalInkRecognizer*)digitalInkRecognizerPtr;
    [recognizer close];
}

NPBINDING DONTSTRIP void* MLKit_DigitalInkRecognizer_GetModelManager(void* digitalInkRecognizerPtr)
{
    MLKitDigitalInkRecognizer*         recognizer = (__bridge MLKitDigitalInkRecognizer*)digitalInkRecognizerPtr;
    return  (__bridge_retained void*)[recognizer getModelManager];
}

MLKInk* createInk(const MLKitDigitalInkDrawing* input)
{
    MLKitDigitalInkDrawingStroke *strokes = input->strokes;
    NSMutableArray<MLKStroke*> *inkStrokes = [NSMutableArray array];

    for (int i = 0; i < input->count; i++) {
        MLKitDigitalInkDrawingStroke* eachStroke = &(strokes[i]);
        NSMutableArray *inkStrokePoints = [NSMutableArray array];

        MLKitDigitalInkDrawingStrokePoint* points = eachStroke->points;
        for (int j = 0; j < eachStroke->count; j++) {
            MLKitDigitalInkDrawingStrokePoint* eachPoint = &(points[j]);
            [inkStrokePoints addObject:[[MLKStrokePoint alloc] initWithX: eachPoint->x y: eachPoint->y /*t: eachPoint->timestamp*/]];
        }
        MLKStroke* inkStroke = [[MLKStroke alloc] initWithPoints:inkStrokePoints];
        [inkStrokes addObject:inkStroke];
    }
    
    return [[MLKInk alloc] initWithStrokes:inkStrokes];
}

MLKDigitalInkRecognizerOptions* createOptions(const MLKitDigitalInkRecognizerOptions* scanOptions)
{
    NSString *identifier =  NPCreateNSStringFromCString(scanOptions->identifier);
    
    MLKDigitalInkRecognitionModelIdentifier *modelIdentifier = [MLKDigitalInkRecognitionModelIdentifier modelIdentifierForLanguageTag:identifier];
    if (modelIdentifier == nil) {
        return nil;
    }
    
    MLKDigitalInkRecognitionModel *model = [[MLKDigitalInkRecognitionModel alloc] initWithModelIdentifier:modelIdentifier];
    MLKDigitalInkRecognizerOptions *options = [[MLKDigitalInkRecognizerOptions alloc] initWithModel:model];
    
    return options;
}
