namespace VoxelBusters.EasyMLKit
{
    public class DrawingPoint
    {
        float m_x, m_y;
        long m_timestamp;

        public DrawingPoint(float x, float y, long timestamp)
        {
            m_x = x;
            m_y = y;
            m_timestamp = timestamp;
        }

        public float GetX()
        {
            return m_x;
        }

        public float GetY()
        {
            return m_y;
        }

        public long GetTimestamp()
        {
            return m_timestamp;
        }
    }
}