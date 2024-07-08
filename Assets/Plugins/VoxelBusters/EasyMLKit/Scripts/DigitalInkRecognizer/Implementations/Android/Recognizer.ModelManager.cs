#if UNITY_ANDROID
using VoxelBusters.CoreLibrary;
using VoxelBusters.EasyMLKit.Implementations.Android.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Android
{
    public partial class DigitalInkRecognizerImplementation
    {
        private class ModelManager : DigitalInkRecognizerModelManager
        {
            private NativeDigitalInkRecognizerModelManager nativeManager;

            public ModelManager(NativeDigitalInkRecognizerModelManager nativeManager)
            {
                this.nativeManager = nativeManager;
            }

            public override void DeleteModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                nativeManager.DeleteModel(new NativeDigitalInkRecognizerModelIdentifier(modelIdentifier.GetIdentifier()), GetListener(modelIdentifier, callback));
            }

            public override void DownloadModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                nativeManager.DownloadModel(new NativeDigitalInkRecognizerModelIdentifier(modelIdentifier.GetIdentifier()), GetListener(modelIdentifier, callback));
            }

            public override bool IsModelAvailable(DigitalInkRecognizerModelIdentifier modelIdentifier)
            {
                return nativeManager.IsModelAvailable(new NativeDigitalInkRecognizerModelIdentifier(modelIdentifier.GetIdentifier()));
            }

            private NativeModelManagerActionListener GetListener(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                return new NativeModelManagerActionListener()
                {
                    onSuccessCallback = () => CallbackDispatcher.InvokeOnMainThread(() => callback?.Invoke(null)),
                    onFailureCallback = (nativeException) => CallbackDispatcher.InvokeOnMainThread(() => callback?.Invoke(new Error(nativeException.GetMessage())))
                };
            }
        }
    }
}
#endif