#if UNITY_IOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    public class NativeImageInputSource
    {
        #region Native Bindings

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_ImageInputSourceFeed_InitWithImageData(IntPtr bytes, int length);

        #endregion

        public IntPtr NativeHandle
        {
            get;
            private set;
        }

        public NativeImageInputSource(byte[] bytes)
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            NativeHandle = MLKit_ImageInputSourceFeed_InitWithImageData(handle.AddrOfPinnedObject(), bytes.Length);

            Debug.LogError("Check this");
            handle.Free();
        }
    }
}
#endif