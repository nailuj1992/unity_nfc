//
//  ARKitCameraInputSourceFeed.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//

#import "ARKitCameraInputSourceFeed.h"
#import <ARKit/ARFrame.h>
#import <CoreMedia/CMTime.h>
#import <MLImage/GMLImage.h>
#import <MLKitVision/GMLImage+MLKitCompatible.h>
#import <ARKit/ARCamera.h>

@interface ARKitCameraInputSourceFeed ()
@property(nonatomic, weak) ARSession* arSession;
@property(atomic) BOOL started;
@property(nonatomic) BOOL isProcessing;
@end


@implementation ARKitCameraInputSourceFeed
@synthesize delegate;
@synthesize arSession;
@synthesize started;
@synthesize isProcessing = _isProcessing;

-(id)initWithARSession:(ARSession *)arSession
{
    self = [super init];
    self.arSession = arSession;
    
    return self;
}

- (void)start {
    self.started = true;
}

- (void)stop {
    self.started = false;
}

- (void)setIsProcessing:(BOOL)status {
    _isProcessing = status;
}



- (void) onCameraFrameReceived
{
    if(started && !_isProcessing)
    {
        if(delegate != NULL)
        {
            [self setIsProcessing: TRUE];

            dispatch_async(dispatch_get_global_queue(DISPATCH_QUEUE_PRIORITY_DEFAULT, 0), ^{
                CVPixelBufferRef pixelBufferRef = arSession.currentFrame.capturedImage;
                GMLImage* image = [[GMLImage alloc] initWithPixelBuffer: pixelBufferRef];
                image.orientation = [self getARKitCameraOrientation];
                [self.delegate didReceiveInputImage:image];
            });
        }
    }
}

-(UIImageOrientation) getARKitCameraOrientation
{
    UIDeviceOrientation deviceOrientation = [[UIDevice currentDevice] orientation];
    switch (deviceOrientation) {
        case UIDeviceOrientationPortrait:
            return UIImageOrientationRight;
        case UIDeviceOrientationLandscapeRight:
            return UIImageOrientationDown;
        default:
            return UIImageOrientationUp;
    }
}

- (float)getInputSourceRotation {
    UIDeviceOrientation deviceOrientation = [[UIDevice currentDevice] orientation];
    switch (deviceOrientation) {
        case UIDeviceOrientationPortrait:
            return 90;
        case UIDeviceOrientationLandscapeRight:
            return 0;
        case UIDeviceOrientationLandscapeLeft:
            return 180;
        default:
            return 0;
    }
}


- (CGSize)getInputSourceSize {
    CGSize size;
    CVPixelBufferRef pixelBuffer = arSession.currentFrame.capturedImage;
    size.width = CVPixelBufferGetWidth(pixelBuffer);
    size.height = CVPixelBufferGetHeight(pixelBuffer);
    return size;
}



//Unused
-(CMSampleBufferRef) getSampleBufferRef:(CVPixelBufferRef) pixelBufferRef withTimestamp:(NSTimeInterval) frameTimestamp
{
    
    CMTimeScale scale = (CMTimeScale)NSEC_PER_SEC;
    CMTime timestamp = CMTimeMake((CMTimeValue)frameTimestamp * (double)scale, scale);
    CMSampleTimingInfo timing = {kCMTimeInvalid , timestamp, kCMTimeInvalid };
    
    CMFormatDescriptionRef formatDesc = nil;
    CMVideoFormatDescriptionCreateForImageBuffer(kCFAllocatorDefault, pixelBufferRef, &formatDesc);
    
    CMSampleBufferRef sampleBuffer;
    CMSampleBufferCreateReadyWithImageBuffer(kCFAllocatorDefault, pixelBufferRef, formatDesc,&timing, &sampleBuffer);
    return sampleBuffer;
}

@end

