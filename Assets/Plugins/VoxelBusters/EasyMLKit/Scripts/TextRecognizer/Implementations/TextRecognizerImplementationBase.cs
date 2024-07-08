using System;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations
{
    public abstract class TextRecognizerImplementationBase : NativeFeatureInterfaceBase, ITextRecognizerImplementation
    {
        #region Constructors

        protected TextRecognizerImplementationBase(bool isAvailable)
            : base(isAvailable)
        { }

        public abstract void Close(OnCloseInternalCallback callback);
        public abstract void Prepare(IImageInputSource imageInputSource, TextRecognizerOptions options, OnPrepareCompleteInternalCallback callback);
        public abstract void Process(OnProcessUpdateInternalCallback<TextRecognizerResult> callback);

        #endregion
    }
}
