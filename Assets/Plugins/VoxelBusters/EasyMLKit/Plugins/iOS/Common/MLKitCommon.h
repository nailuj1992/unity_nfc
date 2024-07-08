//
//  MLKitCommon.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//


#ifndef MLKitCommon_h
#define MLKitCommon_h
#define CHAR_STRING void*
#import "NPBindingHelper.h"


typedef struct
{
    float   x;
    float   y;
    float   width;
    float   height;
} MLKitRect;

typedef struct
{
    void* tag;
    void (*callback)(void*);
} MLKitNativeCallbackData0;


template <typename TParam1>
struct MLKitNativeCallbackData1 {
    void* tag;
    void (*callback)(void*, TParam1);
};

template <typename TParam1, typename TParam2>
struct MLKitNativeCallbackData2 {
    void* tag;
    void (*callback)(void*, TParam1, TParam2);
};

template <typename TParam1, typename TParam2, typename TParam3>
struct MLKitNativeCallbackData3 {
    void* tag;
    void (*callback)(void*, TParam1, TParam2, TParam3);
};

template<typename T>
T getStackAllocatedStruct(T* structPtr) {
    T structCopy = {0};
    
    memcpy((void*)&structCopy, (void*)structPtr, sizeof(T));
    return structCopy;
}

typedef void(^CompletionBlock)(NSError *_Nullable error);

//const NSErrorDomain MLKitErrorDomain = @"Easy ML Kit";

NPBINDING DONTSTRIP MLKitRect getRect(CGRect source);
#endif
