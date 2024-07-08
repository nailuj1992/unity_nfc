using VoxelBusters.CoreLibrary;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Null
{
    public class ObjectDetectorAndTrackerImplementation : ObjectDetectorAndTrackerImplementationBase
    {
        private string NotAllowed = "Not allowed on this platform";
        public ObjectDetectorAndTrackerImplementation()
            : base(isAvailable : true)
        {
        }

        public override void Prepare(IImageInputSource inputSource, ObjectDetectorAndTrackerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAllowed));
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<ObjectDetectorAndTrackerResult> callback)
        {
            if (callback != null)
            {
                callback(new ObjectDetectorAndTrackerResult(null, new Error(NotAllowed)));
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAllowed));
            }
        }

    }
}
