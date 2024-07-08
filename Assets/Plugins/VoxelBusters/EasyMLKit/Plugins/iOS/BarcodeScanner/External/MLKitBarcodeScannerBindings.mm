//
//  MLKitBarcodeScannerExternalBindings.m
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 01/06/22.
//

#import "MLKitBarcodeScannerBindings.h"
#import "MLKitBarcodeScannerListenerWrapper.h"
#import "MLKitBarcodeScanner.h"
#import "IVisionInputSourceFeed.h"

// C bindings
NPBINDING DONTSTRIP void* MLKit_BarcodeScanner_Init()
{
    return (__bridge_retained void*)[[MLKitBarcodeScanner alloc] init];
}

NPBINDING DONTSTRIP void MLKit_BarcodeScanner_SetListener( void* barcodeScannerPtr,
                              void* listenerTag,
                              BarcodeScannerPrepareSuccessNativeCallback prepareSuccessCallback,
                              BarcodeScannerPrepareFailedNativeCallback prepareFailedCallback,
                              BarcodeScannerScanSuccessNativeCallback scanSuccessCallback,
                              BarcodeScannerScanFailedNativeCallback scanFailedCallback)
{
    MLKitBarcodeScanner*     scanner  = (__bridge MLKitBarcodeScanner*)barcodeScannerPtr;
    MLKitBarcodeScannerListenerWrapper *wrapper = [[MLKitBarcodeScannerListenerWrapper alloc] initWithTag: listenerTag];
    wrapper.prepareSuccessCallback = prepareSuccessCallback;
    wrapper.prepareFailedCallback = prepareFailedCallback;
    wrapper.scanSuccessCallback = scanSuccessCallback;
    wrapper.scanFailedCallback = scanFailedCallback;
    [scanner setListener:wrapper];
}

NPBINDING DONTSTRIP void MLKit_BarcodeScanner_Prepare(void* barcodeScannerPtr, void* inputSourcePtr, void* barcodeScanOptionsPtr)
{
    id<IVisionInputSourceFeed> inputSource                  = (__bridge id<IVisionInputSourceFeed>)inputSourcePtr;
    MLKitBarcodeScanner*         scanner                    = (__bridge MLKitBarcodeScanner*)barcodeScannerPtr;
    MLKitBarcodeScanOptions*      barcodeScanOptions        = (__bridge MLKitBarcodeScanOptions*)barcodeScanOptionsPtr;
    
    [scanner prepare:inputSource withOptions:barcodeScanOptions];
}

NPBINDING DONTSTRIP void MLKit_BarcodeScanner_Process(void* barcodeScannerPtr)
{
    MLKitBarcodeScanner*         scanner  = (__bridge MLKitBarcodeScanner*)barcodeScannerPtr;
    [scanner process];
}

NPBINDING DONTSTRIP void MLKit_BarcodeScanner_Close(void* barcodeScannerPtr)
{
    MLKitBarcodeScanner*         scanner = (__bridge MLKitBarcodeScanner*)barcodeScannerPtr;
    [scanner close];
}
