
namespace VoxelBusters.EasyMLKit
{
    using Internal;
    using VoxelBusters.CoreLibrary.NativePlugins;

    public interface IBarcodeScannerImplementation : INativeFeatureInterface
    {
        void Prepare(IImageInputSource inputSource, BarcodeScannerOptions options, OnPrepareCompleteInternalCallback callback);
        void Process(OnProcessUpdateInternalCallback<BarcodeScannerResult> callback);
        void Close(OnCloseInternalCallback callback);
    }
}
