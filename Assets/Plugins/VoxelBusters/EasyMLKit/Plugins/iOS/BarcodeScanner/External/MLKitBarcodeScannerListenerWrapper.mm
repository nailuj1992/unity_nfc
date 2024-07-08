//
//  MLKitBarcodeScannerListenerWrapper.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "MLKitBarcodeScannerListenerWrapper.h"

@interface MLKitBarcodeScannerListenerWrapper ()
@property (nonatomic, assign) void* tag;
@end

@implementation MLKitBarcodeScannerListenerWrapper
@synthesize tag;

-(id) initWithTag:(void*) tag
{
    self = [super init];
    self.tag = tag;
    
    return self;
}


- (void)onPrepareFailed:(nonnull NSError *)error {
    if(_prepareFailedCallback != NULL)
    {
        _prepareFailedCallback(self.tag,NPCreateError(0, [error localizedDescription]));
    }
}

- (void)onPrepareSuccess {
    if(_prepareSuccessCallback != NULL)
    {
        _prepareSuccessCallback(self.tag);
    }
}

- (void)onScanFailed:(nonnull NSError *)error {
    if(_scanFailedCallback != NULL)
    {
        _scanFailedCallback(self.tag, NPCreateError(0, [error localizedDescription]));
    }
}

- (void)onScanSuccess:(nonnull MLKitBarcodeScanResult*)result {
    if(_scanSuccessCallback != NULL)
    {
        NSArray<MLKBarcode*>* barcodes = result.barcodes;
        //Convert from MLKBarcode to MLKitBarcode c array        
        NSMutableArray *barcodesArray = (barcodes == nil || barcodes.count == 0) ? nil : [NSMutableArray array];
        for (MLKBarcode* each in barcodes) {
            MLKitBarcode *barcode = (MLKitBarcode*)malloc(sizeof(MLKitBarcode));
            convertToMLKitBarcode(each, barcode);
            NSValue *value = [NSValue valueWithPointer:barcode];
            [barcodesArray addObject:value];
        }
        
        NPArray *arr = NPCreateNativeArrayFromNSArray(barcodesArray);
        
        NPArrayProxy proxy;
        proxy.length = arr->length;
        proxy.ptr = arr->ptr;
        
        _scanSuccessCallback(self.tag, proxy, NPCreateSizeFromCGSize(result.inputSize), result.inputRotation);
    }
}

@end
