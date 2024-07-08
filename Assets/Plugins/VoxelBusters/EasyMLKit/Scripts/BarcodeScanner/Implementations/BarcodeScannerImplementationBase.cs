using System;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations
{
    public abstract class BarcodeScannerImplementationBase : NativeFeatureInterfaceBase, IBarcodeScannerImplementation
    {
        #region Constructors

        protected BarcodeScannerImplementationBase(bool isAvailable)
            : base(isAvailable)
        { }

        public abstract void Close(OnCloseInternalCallback callback);
        public abstract void Prepare(IImageInputSource inputSource, BarcodeScannerOptions options, OnPrepareCompleteInternalCallback callback);
        public abstract void Process(OnProcessUpdateInternalCallback<BarcodeScannerResult> callback);

        #endregion
    }
}
