//
//  MLKitDigitalInkModelManager.m
//  UnityFramework
//
//  Created by Ayyappa J on 05/04/23.
//

#import "MLKitDigitalInkModelManager.h"
#import <MLKitCommon/MLKModelManager.h>
#import <MLKitDigitalInkRecognition/MLKDigitalInkRecognitionModel.h>
#import <MLKitCommon/MLKModelDownloadConditions.h>
#import <MLKitCommon/MLKModelDownloadNotifications.h>

@interface MLKitDigitalInkModelManager ()
@property (nonatomic, copy) CompletionBlock  currentDownloadListener;
@end

@implementation MLKitDigitalInkModelManager

-(BOOL) isModelAvailable:(MLKDigitalInkRecognitionModelIdentifier*) modelIdentifier
{
    MLKDigitalInkRecognitionModel *model = [[MLKDigitalInkRecognitionModel alloc]
                                              initWithModelIdentifier:modelIdentifier];
    
    MLKModelManager *modelManager = [MLKModelManager modelManager];
    return [modelManager isModelDownloaded:model];
}

-(void) downloadModel:(MLKDigitalInkRecognitionModelIdentifier*) modelIdentifier completion:(CompletionBlock) completionBlock
{
    MLKModelManager *modelManager = [MLKModelManager modelManager];
    MLKDigitalInkRecognitionModel *model = [[MLKDigitalInkRecognitionModel alloc]
                                              initWithModelIdentifier:modelIdentifier];
    
    _currentDownloadListener = completionBlock;

    [modelManager downloadModel:model conditions:[[MLKModelDownloadConditions alloc] initWithAllowsCellularAccess:YES
                                                                                     allowsBackgroundDownloading:YES]];

    
    //Unregister first
    [[NSNotificationCenter defaultCenter] removeObserver:self];
    
    [[NSNotificationCenter defaultCenter] addObserver:self
                                                     selector:@selector(onDownloadSuccess:)
                                                         name:MLKModelDownloadDidSucceedNotification
                                                       object:nil];
    [[NSNotificationCenter defaultCenter] addObserver:self
                                                     selector:@selector(onDownloadFailed:)
                                                         name:MLKModelDownloadDidFailNotification
                                                       object:nil];
}

-(void) deleteModel:(MLKDigitalInkRecognitionModelIdentifier*) modelIdentifier completion:(CompletionBlock) completionBlock
{
    MLKModelManager *modelManager = [MLKModelManager modelManager];
    MLKDigitalInkRecognitionModel *model = [[MLKDigitalInkRecognitionModel alloc]
                                              initWithModelIdentifier:modelIdentifier];
    
    [modelManager deleteDownloadedModel:model
                             completion:^(NSError * _Nullable error) {
                                            if(completionBlock != nil)
                                            {
                                                if(error == nil)
                                                    completionBlock(nil);
                                                else
                                                    completionBlock(error);
                                            }
    }];
    
}

-(void) onDownloadSuccess:(NSNotification*) notification
{
    if(_currentDownloadListener != nil)
    {
        _currentDownloadListener(nil);
    }
}

-(void) onDownloadFailed:(NSNotification*) notification
{
    NSError *error = (NSError*) [notification.userInfo objectForKey:MLKModelDownloadUserInfoKeyError];
    if(_currentDownloadListener != nil)
    {
        _currentDownloadListener(error);
    }
}


@end
