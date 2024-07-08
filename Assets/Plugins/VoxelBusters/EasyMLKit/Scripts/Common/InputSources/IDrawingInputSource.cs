namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Base interface for all kinds of input sources
    /// </summary>
    public interface IDrawingInputSource : IInputSource
    {
        void AddStroke(DrawingStroke stroke);
        DrawingStroke[] GetStrokes();
    }
}
