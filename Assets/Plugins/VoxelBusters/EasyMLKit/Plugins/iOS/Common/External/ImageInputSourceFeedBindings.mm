//
//  ImageInputSourceFeedBindings.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 03/06/22.
//

#import "ImageInputSourceFeedBindings.h"
#import "ImageInputSourceFeed.h"

NPBINDING DONTSTRIP void* MLKit_ImageInputSourceFeed_InitWithImageData(void* dataBytesPtr, int length)
{
    NSData* data = [NSData dataWithBytes:dataBytesPtr length:length];
    UIImage *image = [UIImage imageWithData:data];
    
    return (__bridge_retained void*)[[ImageInputSourceFeed alloc] initWithImage:image]; //Cross check this
}
