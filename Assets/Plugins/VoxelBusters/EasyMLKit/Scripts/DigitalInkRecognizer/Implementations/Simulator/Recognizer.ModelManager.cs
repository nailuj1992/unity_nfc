#if UNITY_EDITOR

namespace VoxelBusters.EasyMLKit.Implementations.Simulator
{
    public partial class DigitalInkRecognizerImplementation
    {
        private class ModelManager : DigitalInkRecognizerModelManager
        {
            public override void DeleteModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                callback?.Invoke(new CoreLibrary.Error("Can't delete models on simulator"));
            }

            public override void DownloadModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback callback)
            {
                callback?.Invoke(null);
            }

            public override bool IsModelAvailable(DigitalInkRecognizerModelIdentifier modelIdentifier)
            {
                return true;
            }
        }
    }
}
#endif