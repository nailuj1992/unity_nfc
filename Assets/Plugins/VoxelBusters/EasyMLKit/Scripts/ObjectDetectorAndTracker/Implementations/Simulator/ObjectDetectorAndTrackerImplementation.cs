#if UNITY_EDITOR
using System.Collections.Generic;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Simulator
{
    public class ObjectDetectorAndTrackerImplementation : ObjectDetectorAndTrackerImplementationBase
    {
        public ObjectDetectorAndTrackerImplementation()
            : base(isAvailable : true)
        {
        }

        public override void Prepare(IImageInputSource inputSource, ObjectDetectorAndTrackerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<ObjectDetectorAndTrackerResult> callback)
        {
            if (callback != null)
            {
                callback(new ObjectDetectorAndTrackerResult(new List<DetectedObject>(), null));
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }
    }
}
#endif