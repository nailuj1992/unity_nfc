//
//  MLKitText.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 31/06/22.
//
#import "MLKitText.h"
#import <MLKitTextRecognition/MLKitTextRecognition.h>
#import <MLKitTextRecognitionCommon/MLKText.h>
#import <MLKitTextRecognitionCommon/MLKTextBlock.h>
#import <MLKitTextRecognitionCommon/MLKTextLine.h>
#import <MLKitTextRecognitionCommon/MLKTextElement.h>

NPBINDING DONTSTRIP NSArray* getBlocks(NSArray<MLKTextBlock*>* source);
NPBINDING DONTSTRIP NSArray* getLines(NSArray<MLKTextLine*>* source);
NPBINDING DONTSTRIP NSArray* getElements(NSArray<MLKTextElement*>* source);

NPBINDING DONTSTRIP void convertToMLKitText(MLKText* source, MLKitText* destination)
{
    memset(destination, 0, sizeof(MLKitText));
    destination->rawValue = (void*)[source.text UTF8String];
    //destination->blocks   = *(NPCreateNativeArrayFromNSArray(getBlocks(source.blocks))); //@@ Find out why it's not working
    NPArray *array = NPCreateNativeArrayFromNSArray(getBlocks(source.blocks));
    destination->blocks.length = array->length;
    destination->blocks.ptr = array->ptr;
}

NPBINDING DONTSTRIP NSArray* getBlocks(NSArray<MLKTextBlock*>* source)
{
    NSMutableArray *blocks = [NSMutableArray array];
    for (MLKTextBlock* each in source) {
        MLKitTextBlock *block = (MLKitTextBlock*)malloc(sizeof(MLKitTextBlock));
        memset(block, 0, sizeof(MLKitTextBlock));
        block->frame = getRect(each.frame);
        block->lines = *(NPCreateNativeArrayFromNSArray(getLines(each.lines)));
        block->rawValue = (void*)[each.text UTF8String];
        NSValue *value = [NSValue valueWithPointer:block];
        [blocks addObject:value];
    }
    
    return blocks;
}

NPBINDING DONTSTRIP NSArray* getLines(NSArray<MLKTextLine*>* source)
{
    NSMutableArray *lines = [NSMutableArray array];
    for (MLKTextLine* each in source) {
        MLKitTextLine *line = (MLKitTextLine*)malloc(sizeof(MLKitTextLine));
        memset(line, 0, sizeof(MLKitTextLine));
        line->frame = getRect(each.frame);
        line->rawValue = (void*)[each.text UTF8String];
        line->elements = *(NPCreateNativeArrayFromNSArray(getElements(each.elements)));
        NSValue *value = [NSValue valueWithPointer:line];
        [lines addObject:value];
    }
    
    return lines;
}

NPBINDING DONTSTRIP NSArray* getElements(NSArray<MLKTextElement*>* source)
{
    NSMutableArray *lines = [NSMutableArray array];
    for (MLKTextLine* each in source) {
        MLKitTextLine *line = (MLKitTextLine*)malloc(sizeof(MLKitTextLine));
        memset(line, 0, sizeof(MLKitTextLine));
        line->frame = getRect(each.frame);
        line->rawValue = (void*)[each.text UTF8String];
        NSValue *value = [NSValue valueWithPointer:line];
        [lines addObject:value];
    }
    
    return lines;
}
