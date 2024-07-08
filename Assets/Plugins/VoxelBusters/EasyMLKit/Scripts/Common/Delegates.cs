using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    public delegate void OnPrepareCompleteCallback<TFeature>(TFeature feature, Error error);
    public delegate void OnProcessUpdateCallback<TFeature, TResult>(TFeature feature, TResult result);
    public delegate void OnCloseCallback<TFeature>(TFeature feature, Error error);
    public delegate void OnCompleteCallback(Error error);
}

namespace VoxelBusters.EasyMLKit.Internal
{
    public delegate void OnPrepareCompleteInternalCallback(Error error);
    public delegate void OnProcessUpdateInternalCallback<TResult>(TResult result);
    public delegate void OnCloseInternalCallback(Error error);
}