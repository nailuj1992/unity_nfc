//
//  MLKitBarcodeScanResult.h
//  UnityFramework
//
//  Created by Ayyappa J on 10/06/22.
//

#import <Foundation/Foundation.h>
#import <MLKitTextRecognitionCommon/MLKText.h>

NS_ASSUME_NONNULL_BEGIN

@interface MLKitTextRecognizerResult : NSObject
@property (nonatomic, strong) MLKText* text;
@property (nonatomic) float inputRotation;
@property (nonatomic) CGSize inputSize;
@end
NS_ASSUME_NONNULL_END
