#if UNITY_IOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeTextElement
    {
        public UnityRect Frame;
        public IntPtr DisplayValue;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeTextLine
    {
        public UnityRect Frame;
        public NativeArray Elements;
        public IntPtr DisplayValue;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeTextBlock
    {
        public UnityRect Frame;
        public NativeArray Lines;
        public IntPtr DisplayValue;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeText
    {
        public IntPtr RawValue;
        public NativeArray Blocks;
    };
}
#endif