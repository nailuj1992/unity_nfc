//
//  TextRecognizer.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import "MLKitTextRecognizer.h"
#import "IMLKitTextRecognizerListener.h"
#import <MLKitTextRecognitionCommon/MLKCommonTextRecognizerOptions.h>

@interface MLKitTextRecognizer()
@property(nonatomic, strong) MLKCommonTextRecognizerOptions*  options;
@property(nonatomic, strong) id<IVisionInputSourceFeed>  inputSourceFeed;
@property(nonatomic, strong) MLKTextRecognizer *scanner;
@property(nonatomic, strong) id<IMLKitTextRecognizerListener> listener;
@property(nonatomic, strong) MLKitTextRecognizerResult* cachedResult;
@end


@implementation MLKitTextRecognizer
@synthesize options;
@synthesize inputSourceFeed;
@synthesize scanner;
@synthesize listener = _listener;
@synthesize cachedResult;

- (id)init
{
    self = [super init];
    self.cachedResult = [[MLKitTextRecognizerResult alloc] init];
    return self;
}

- (void)setListener:(id<IMLKitTextRecognizerListener>)listener
{
    _listener = listener;
}

-(void) prepare:(id<IVisionInputSourceFeed>) inputSource withOptions:(nonnull MLKitTextRecognizerScanOptions*) scanOptions;
{
    self.inputSourceFeed = inputSource;
    [self.inputSourceFeed setDelegate: self];
    self.options = [self createOptions: scanOptions];
    self.scanner = [MLKTextRecognizer textRecognizerWithOptions:self.options];

    if(_listener != nil)
    {
        [_listener onPrepareSuccess];
    }
    
}

- (void)process
{
    [self.inputSourceFeed start];
}

- (void)close
{
    [self.inputSourceFeed stop];
    self.scanner = nil;
    self.inputSourceFeed = nil;
}

- (void)processWithImage: (MLKVisionImage*) inputImage
{
    NSError *error = nil;
    MLKText *text = [self.scanner resultsInImage:inputImage error:&error];
    if(error == nil)
    {
        [self updateCachedResult:text];
        [_listener onScanSuccess:self.cachedResult];
    }
    else
    {
        NSLog(@"Error : %@", [error localizedDescription]);
        [_listener onScanFailed:error];
    }
    
    [self.inputSourceFeed setIsProcessing:FALSE];
}

-(MLKCommonTextRecognizerOptions*) createOptions:(nonnull MLKitTextRecognizerScanOptions *) scanOptions
{
    MLKCommonTextRecognizerOptions *options = nil;
    
    if([scanOptions.language isEqualToString: @"Chinese"])
    {
        options = [[NSClassFromString(@"MLKChineseTextRecognizerOptions") alloc] init];
    }
    else if([scanOptions.language isEqualToString: @"Devanagari"])
    {
        options = [[NSClassFromString(@"MLKDevanagariTextRecognizerOptions") alloc] init];
    }
    else if([scanOptions.language isEqualToString: @"Japanese"])
    {
        options = [[NSClassFromString(@"MLKJapaneseTextRecognizerOptions") alloc] init];
    }
    else if([scanOptions.language isEqualToString: @"Korean"])
    {
        options = [[NSClassFromString(@"MLKKoreanTextRecognizerOptions") alloc] init];
    }
    else
    {
        options = [[MLKTextRecognizerOptions alloc] init];//Default options
    }
    
    return options;
}

-(void) updateCachedResult:(MLKText*) text
{
    self.cachedResult.text      = text;
    self.cachedResult.inputRotation  = [self.inputSourceFeed getInputSourceRotation];
    self.cachedResult.inputSize     = [self.inputSourceFeed getInputSourceSize];
}

- (void)didReceiveInputImage:(nonnull MLKVisionImage *)inputImage {
    [self processWithImage:inputImage];
}

@end
