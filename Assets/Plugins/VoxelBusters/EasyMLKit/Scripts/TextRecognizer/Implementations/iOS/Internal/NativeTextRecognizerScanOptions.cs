#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    public class NativeTextRecognizerScanOptions
    {
#region Native Bindings

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_TextRecognizerScanOptions_Init();

        [DllImport("__Internal")]
        public static extern void MLKit_TextRecognizerScanOptions_SetLanguage(IntPtr options, string language);

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_TextRecognizerScanOptions_GetLanguage(IntPtr options);

#endregion


        public IntPtr NativeHandle
        {
            get;
            private set;
        }

        public string Language
        {
            get
            {
                return MLKit_TextRecognizerScanOptions_GetLanguage(NativeHandle).AsString();
            }
            set
            {
                MLKit_TextRecognizerScanOptions_SetLanguage(NativeHandle, value);
            }
        }

        public NativeTextRecognizerScanOptions()
        {
            NativeHandle = MLKit_TextRecognizerScanOptions_Init();
        }

    }
}
#endif