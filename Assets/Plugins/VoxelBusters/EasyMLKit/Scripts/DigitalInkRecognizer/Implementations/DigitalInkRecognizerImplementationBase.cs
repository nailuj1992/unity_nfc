using System;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations
{
    public abstract class DigitalInkRecognizerImplementationBase : NativeFeatureInterfaceBase, IDigitalInkRecognizerImplementation
    {
        #region Constructors

        protected DigitalInkRecognizerImplementationBase(bool isAvailable)
            : base(isAvailable)
        { }

        public abstract void Close(OnCloseInternalCallback callback);
        public abstract DigitalInkRecognizerModelManager GetModelManager();
        public abstract void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteInternalCallback callback);
        public abstract void Process(OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> callback);

        #endregion
    }
}
