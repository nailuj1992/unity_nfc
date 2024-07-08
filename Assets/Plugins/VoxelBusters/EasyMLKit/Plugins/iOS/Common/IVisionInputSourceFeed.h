//
//  IInputSource.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import <Foundation/Foundation.h>
#import <MLKitVision/MLKVisionImage.h>

NS_ASSUME_NONNULL_BEGIN

@protocol IVisionInputSourceFeedDelegate <NSObject>
-(void) didReceiveInputImage: (nonnull id<MLKCompatibleImage>) inputImage;
@end

@protocol IVisionInputSourceFeed <NSObject>
@property (nonatomic, weak, nullable) id<IVisionInputSourceFeedDelegate> delegate;

-(void) start;
-(void) stop;
-(void) setIsProcessing:(BOOL)status;
-(CGSize) getInputSourceSize;
-(float) getInputSourceRotation;
@end

NS_ASSUME_NONNULL_END
