#if UNITY_IOS && EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    public class NativeArKitInputSource
    {
        #region Native Bindings

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_CreateARKitCameraInputSourceFeed_InitWithARSession(IntPtr arSessionStructPtr);

        [DllImport("__Internal")]
        public static extern void MLKit_ARKitCameraInputSourceFeed_OnCameraFrameReceived(IntPtr inputSourcePtr);

        #endregion

        public IntPtr NativeHandle
        {
            get;
            private set;
        }

        public NativeArKitInputSource(ARSession arSession)
        {
            NativeHandle = MLKit_CreateARKitCameraInputSourceFeed_InitWithARSession(arSession.subsystem.nativePtr);
        }

        public void OnCameraFrameReceived()
        {
            MLKit_ARKitCameraInputSourceFeed_OnCameraFrameReceived(NativeHandle);
        }
    }
}
#endif