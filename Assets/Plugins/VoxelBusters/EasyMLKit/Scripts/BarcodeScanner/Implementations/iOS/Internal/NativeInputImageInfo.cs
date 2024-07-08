#if UNITY_IOS
using System.Runtime.InteropServices;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeInputImageInfo
    {
        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }

        public float Rotation
        {
            get;
            set;
        }
    }
}
#endif