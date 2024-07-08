using System;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations
{
    public abstract class ObjectDetectorAndTrackerImplementationBase : NativeFeatureInterfaceBase, IObjectDetectorAndTrackerImplementation
    {
        #region Constructors

        protected ObjectDetectorAndTrackerImplementationBase(bool isAvailable)
            : base(isAvailable)
        { }

        public abstract void Prepare(IImageInputSource inputSource, ObjectDetectorAndTrackerOptions options, OnPrepareCompleteInternalCallback callback);
        public abstract void Process(OnProcessUpdateInternalCallback<ObjectDetectorAndTrackerResult> callback);
        public abstract void Close(OnCloseInternalCallback callback);

        #endregion
    }
}
