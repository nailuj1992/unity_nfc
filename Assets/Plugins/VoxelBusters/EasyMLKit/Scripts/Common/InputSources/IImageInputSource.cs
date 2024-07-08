namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Base interface for all kinds of input sources
    /// </summary>
    public interface IImageInputSource : IInputSource
    {
        void Close();
    }
}
