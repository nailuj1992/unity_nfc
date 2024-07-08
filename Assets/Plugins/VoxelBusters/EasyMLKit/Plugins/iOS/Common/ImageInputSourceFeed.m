//
//  ImageInputSourceFeed.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//

#import "ImageInputSourceFeed.h"
#import <MLKitVision/MLKVisionImage.h>
#import <MLImage/GMLImage.h>
#import <MLKitVision/GMLImage+MLKitCompatible.h>

@interface ImageInputSourceFeed()
@property (nonatomic, strong) UIImage* inputImage;
@property (nonatomic) BOOL isProcessing;
@end

@implementation ImageInputSourceFeed
@synthesize delegate;
@synthesize inputImage;
@synthesize isProcessing = _isProcessing;

-(id) initWithImage:(UIImage*) image
{
    self = [super init];
    self.inputImage = image;
    return self;
}

- (void)start {
    dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
        GMLImage* image = [[GMLImage alloc] initWithImage:self.inputImage];
        [self.delegate didReceiveInputImage:image];
    });
}



- (void)stop {
    self.inputImage = nil;
}

- (void)setIsProcessing:(BOOL)status {
    _isProcessing = status;
}

- (float)getInputSourceRotation {
    return 0;
}

- (CGSize)getInputSourceSize {
    return inputImage.size;
}

@end

