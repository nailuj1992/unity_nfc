using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Implementations.Null
{
    public partial class DigitalInkRecognizerImplementation
    {
        private class ModelManager : DigitalInkRecognizerModelManager
        {
            public override void DeleteModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                callback?.Invoke(new Error(NotAvailable));
            }

            public override void DownloadModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                callback?.Invoke(new Error(NotAvailable));
            }

            public override bool IsModelAvailable(DigitalInkRecognizerModelIdentifier modelIdentifier)
            {
                return false;
            }
        }
    }
}