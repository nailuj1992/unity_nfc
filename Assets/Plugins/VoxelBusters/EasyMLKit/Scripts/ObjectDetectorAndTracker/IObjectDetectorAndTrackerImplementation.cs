
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Internal
{
    public interface IObjectDetectorAndTrackerImplementation : INativeFeatureInterface
    {
        void Prepare(IImageInputSource inputSource, ObjectDetectorAndTrackerOptions options, OnPrepareCompleteInternalCallback callback);
        void Process(OnProcessUpdateInternalCallback<ObjectDetectorAndTrackerResult> callback);
        void Close(OnCloseInternalCallback callback);
    }
}
