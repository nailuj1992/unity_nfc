#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    public class NativeBarcodeScanOptions
    {
#region Native Bindings

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_BarcodeScanOptions_Init();

        [DllImport("__Internal")]
        public static extern void MLKit_BarcodeScanOptions_SetAllowedFormats(IntPtr options, int formats);

        [DllImport("__Internal")]
        public static extern int MLKit_BarcodeScanOptions_GetAllowedFormats(IntPtr options);

#endregion


        public IntPtr NativeHandle
        {
            get;
            private set;
        }

        public int AllowedFormats
        {
            get
            {
                return MLKit_BarcodeScanOptions_GetAllowedFormats(NativeHandle);
            }
            set
            {
                MLKit_BarcodeScanOptions_SetAllowedFormats(NativeHandle, value);
            }
        }

        public NativeBarcodeScanOptions()
        {
            NativeHandle = MLKit_BarcodeScanOptions_Init();
        }

    }
}
#endif