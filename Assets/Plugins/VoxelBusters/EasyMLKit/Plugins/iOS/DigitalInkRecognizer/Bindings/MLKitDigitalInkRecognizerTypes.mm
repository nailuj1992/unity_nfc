//
//  MLKitText.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 31/06/22.
//
#import "MLKitDigitalInkRecognizerTypes.h"


NPBINDING DONTSTRIP void convertToMLKitDigitalInkRecognizedValue(MLKDigitalInkRecognitionCandidate* source, MLKitDigitalInkRecognizedValue* destination)
{
    memset(destination, 0, sizeof(MLKitDigitalInkRecognizedValue));
    destination->text = (void*)[source.text UTF8String];
    destination->score = (source.score != nil) ? [source.score floatValue] : -1;
    destination->scoreExists = (source.score != nil) ? 1 : 0;
}
