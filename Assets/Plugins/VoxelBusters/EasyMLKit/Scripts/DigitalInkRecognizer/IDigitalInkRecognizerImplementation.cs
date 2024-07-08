
namespace VoxelBusters.EasyMLKit
{
    using Internal;
    using VoxelBusters.CoreLibrary.NativePlugins;

    public interface IDigitalInkRecognizerImplementation : INativeFeatureInterface
    {
        void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteInternalCallback callback);
        void Process(OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> callback);
        void Close(OnCloseInternalCallback callback);

        DigitalInkRecognizerModelManager GetModelManager();
    }
}
