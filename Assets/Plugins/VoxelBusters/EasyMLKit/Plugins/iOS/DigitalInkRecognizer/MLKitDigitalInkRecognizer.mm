//
//  DigitalInkRecognizer.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import "MLKitDigitalInkRecognizer.h"

@interface MLKitDigitalInkRecognizer()
@property(nonatomic, strong) MLKDigitalInkRecognizerOptions*  options;
@property(nonatomic, strong) MLKInk* inputSource;
@property(nonatomic, strong) MLKDigitalInkRecognizer *scanner;
@property(nonatomic, strong) id<IMLKitDigitalInkRecognizerListener> listener;
@property(nonatomic, strong) MLKitDigitalInkModelManager* modelManager;
@end


@implementation MLKitDigitalInkRecognizer
@synthesize options;
@synthesize inputSource;
@synthesize scanner;
@synthesize listener;


- (void)setListener:(id<IMLKitDigitalInkRecognizerListener>)listener
{
    self.listener = listener;
}
-(void) prepare:(MLKInk*) inkInput withOptions: (MLKDigitalInkRecognizerOptions*) options completionBlock: (CompletionBlock) completionBlock;
{
    NSLog(@"Preparing start...");
    self.inputSource = inkInput;
    self.scanner = [MLKDigitalInkRecognizer digitalInkRecognizerWithOptions:options];
    
    if(completionBlock != nil)
    {
        if(self.scanner != nil)
        {
            completionBlock(nil);
        }
        else
        {
            completionBlock([NSError errorWithDomain:@"MLKitErrorDomain" code:0 userInfo:@{@"Error" : @"Unknown error"}]);//TODO
        }
    }
}

- (void)process:(ProcessCompletionBlock) completionBlock
{
    [self.scanner
          recognizeInk:self.inputSource
            completion:^(MLKDigitalInkRecognitionResult *_Nullable result,
                         NSError *_Nullable error) {
            if(error == nil)
            {
                if(completionBlock != nil)
                {
                    completionBlock(result.candidates, NULL);
                }
            }
            else
            {
                if(completionBlock != nil)
                {
                    completionBlock(NULL, error);
                }
            }
      }];
}

- (void)close
{
    self.scanner = nil;
    self.inputSource = nil;
}

-(MLKitDigitalInkModelManager*) getModelManager
{
    if(self.modelManager == nil)
    {
        self.modelManager = [[MLKitDigitalInkModelManager alloc] init];
    }
    
    return self.modelManager;
}

@end
