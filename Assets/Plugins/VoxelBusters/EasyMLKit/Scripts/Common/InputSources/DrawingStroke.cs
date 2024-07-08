using System.Collections.Generic;

namespace VoxelBusters.EasyMLKit
{
    public class DrawingStroke
    {
        List<DrawingPoint> m_points = new List<DrawingPoint>();

        public void AddPoint(DrawingPoint point)
        {
            m_points.Add(point);
        }

        public DrawingPoint[] GetPoints()
        {
            return m_points.ToArray();
        }
    }
}