//
//  IMLKitDigitalInkModelManagerActionListener.h
//  UnityFramework
//
//  Created by Ayyappa J on 05/04/23.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@protocol IMLKitActionListener <NSObject>
-(void) onSuccess;
-(void) onFailed:(NSError*) error;
@end

NS_ASSUME_NONNULL_END
