//
//  BarcodeScanner.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 25/05/22.
//

#import "MLKitBarcodeScanner.h"
#import <MLKitBarcodeScanning/MLKitBarcodeScanning.h>
#import "IMLKitBarcodeScannerListener.h"
#import "MLKitBarcodeScanResult.h"

@interface MLKitBarcodeScanner()
@property(nonatomic, strong) MLKBarcodeScannerOptions*  options;
@property(nonatomic, strong) id<IVisionInputSourceFeed>  inputSourceFeed;
@property(nonatomic, strong) MLKBarcodeScanner *scanner;
@property(nonatomic, strong) id<IMLKitBarcodeScannerListener> listener;
@property(nonatomic, strong) MLKitBarcodeScanResult* cachedResult;
@end


@implementation MLKitBarcodeScanner
@synthesize options;
@synthesize inputSourceFeed;
@synthesize scanner;
@synthesize listener = _listener;
@synthesize cachedResult;

- (id)init
{
    self = [super init];
    self.cachedResult = [[MLKitBarcodeScanResult alloc] init];
    return self;
}

- (void)setListener:(id<IMLKitBarcodeScannerListener>)listener
{
    _listener = listener;
}

-(void) prepare:(id<IVisionInputSourceFeed>) inputSource withOptions:(nonnull MLKitBarcodeScanOptions*) scanOptions
{
    self.inputSourceFeed = inputSource;
    [self.inputSourceFeed setDelegate: self];
    self.options = [[MLKBarcodeScannerOptions alloc] initWithFormats: scanOptions.allowedFormats];
    self.scanner = [MLKBarcodeScanner barcodeScannerWithOptions:self.options];

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
    NSArray<MLKBarcode *> *barcodes = [self.scanner resultsInImage:inputImage error:&error];
    
    if(error == nil)
    {
        [self updateCachedResult:barcodes];
        [_listener onScanSuccess:self.cachedResult];
    }
    else
    {
        NSLog(@"Error : %@", [error localizedDescription]);
        [_listener onScanFailed:error];
    }
    
    [self.inputSourceFeed setIsProcessing:FALSE];
}

-(void) updateCachedResult:(NSArray<MLKBarcode*>*) barcodes
{
    self.cachedResult.barcodes      = barcodes;
    self.cachedResult.inputRotation  = [self.inputSourceFeed getInputSourceRotation];
    self.cachedResult.inputSize     = [self.inputSourceFeed getInputSourceSize];
}

- (void)didReceiveInputImage:(nonnull MLKVisionImage *)inputImage {
    [self processWithImage:inputImage];
}

@end
