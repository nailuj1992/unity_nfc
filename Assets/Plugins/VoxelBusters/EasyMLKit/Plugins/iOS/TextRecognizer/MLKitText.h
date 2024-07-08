//
//  MLKitText.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 30/06/22.
//

#ifndef MLKitText_h
#define MLKitText_h
#define CHAR_STRING void*


#import <MLKitTextRecognition/MLKitTextRecognition.h>
#import "NPKit.h"
#import "MLKitCommon.h"

typedef struct
{
    MLKitRect frame;
    CHAR_STRING rawValue;
} MLKitTextElement;

typedef struct
{
    MLKitRect   frame;
    NPArray     elements;
    CHAR_STRING rawValue;
} MLKitTextLine;

typedef struct
{
    MLKitRect   frame;
    NPArray     lines;
    CHAR_STRING rawValue;
} MLKitTextBlock;

typedef struct
{
    CHAR_STRING  rawValue;
    NPArrayProxy  blocks; //@@ Check why NPArray isn't working here at root
} MLKitText;

NPBINDING DONTSTRIP void convertToMLKitText(MLKText* source, MLKitText* destination);
#endif /* MLKitText_h */
