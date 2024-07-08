#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS
{
    using Internal;
    using VoxelBusters.CoreLibrary;
    using VoxelBusters.EasyMLKit.Internal;

    public partial class DigitalInkRecognizerImplementation
    {
        
        private class ModelManager : DigitalInkRecognizerModelManager
        {
            private NativeDigitalInkRecognizerModelManager m_nativeManager;

            public ModelManager(NativeDigitalInkRecognizerModelManager nativeManager)
            {
                m_nativeManager = nativeManager;
            }

            public override bool IsModelAvailable(DigitalInkRecognizerModelIdentifier modelIdentifier)
            {
                return m_nativeManager.IsModelAvailable(new NativeDigitalInkRecognizerModelIdentifier(modelIdentifier.GetIdentifier()));
            }

            public override void DownloadModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                NativeCallbackData onSuccess = new NativeCallback().Get(() =>
                {
                    CallbackDispatcher.InvokeOnMainThread(() => callback(null));
                });

                NativeCallbackData<NativeError> onFailed = new NativeCallback().Get<NativeError>((error) =>
                {
                    CallbackDispatcher.InvokeOnMainThread(() => callback(new Error(error.Description)));
                });

                m_nativeManager.DownloadModel(new NativeDigitalInkRecognizerModelIdentifier(modelIdentifier.GetIdentifier()), onSuccess, onFailed);
            }

            public override void DeleteModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                NativeCallbackData onSuccess = new NativeCallback().Get(() =>
                {
                    CallbackDispatcher.InvokeOnMainThread(() => callback(null));
                });

                NativeCallbackData<NativeError> onFailed = new NativeCallback().Get<NativeError>((error) =>
                {
                    CallbackDispatcher.InvokeOnMainThread(() => callback(new Error(error.Description)));
                });

                m_nativeManager.DeleteModel(new NativeDigitalInkRecognizerModelIdentifier(modelIdentifier.GetIdentifier()), onSuccess, onFailed);
            }
        }
    }
}
#endif