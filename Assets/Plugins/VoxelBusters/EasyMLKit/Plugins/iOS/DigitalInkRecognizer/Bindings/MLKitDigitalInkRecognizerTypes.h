//
//  MLKitText.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 30/06/22.
//

#ifndef MLKitText_h
#define MLKitText_h
#define CHAR_STRING void*

#import <MLKitDigitalInkRecognition/MLKDigitalInkRecognitionCandidate.h>
#import "NPKit.h"
#import "MLKitCommon.h"


typedef struct
{
    CHAR_STRING     text;
    float           score;
    int             scoreExists;
} MLKitDigitalInkRecognizedValue;


typedef struct
{
    float x;
    float y;
    long timestamp;
} MLKitDigitalInkDrawingStrokePoint;

typedef struct
{
    MLKitDigitalInkDrawingStrokePoint* points;
    int count;
} MLKitDigitalInkDrawingStroke;


typedef struct
{
    MLKitDigitalInkDrawingStroke* strokes;
    int count;
} MLKitDigitalInkDrawing;

typedef struct
{
    const char* identifier;
    float inputWidth;
    float inputHeight;
    const char* preContext;
    
} MLKitDigitalInkRecognizerOptions;

typedef struct
{
    const char* identifier;
} MLKitDigitalInkModelIdentifier;

NPBINDING DONTSTRIP void convertToMLKitDigitalInkRecognizedValue(MLKDigitalInkRecognitionCandidate* source, MLKitDigitalInkRecognizedValue* destination);
#endif /* MLKitText_h */
