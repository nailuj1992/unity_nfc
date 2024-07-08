//
//  MLKitActionListenerWrapper.m
//  UnityFramework
//
//  Created by Ayyappa J on 05/04/23.
//

#import "MLKitActionListenerWrapper.h"

@interface MLKitActionListenerWrapper ()
@property (nonatomic, assign) void* tag;
@end

@implementation MLKitActionListenerWrapper
@synthesize tag;

-(id) initWithTag:(void*) tag
{
    self = [super init];
    self.tag = tag;
    
    return self;
}

-(void) onSuccess
{
    if(_onSuccessCallback != nil)
    {
        _onSuccessCallback(self.tag);
    }
    
}

-(void) onFailed:(NSError*) error
{
    if(_onFailureCallback != nil)
    {
        _onFailureCallback(self.tag, (NPCreateError(0, [error localizedDescription])));
    }
}
@end
