#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal class NativeGoogleMLKitBarcodeScannerListener
    {
        #region Delegates

        internal delegate void BarcodeScannerPrepareSuccessNativeCallback(IntPtr tagPtr);
        internal delegate void BarcodeScannerPrepareFailedNativeCallback(IntPtr tagPtr, NativeError error);
        internal delegate void BarcodeScannerScanSuccessNativeCallback(IntPtr tagPtr, NativeArray barcodes, NativeSize inputSize, float inputRotation);
        internal delegate void BarcodeScannerScanFailedNativeCallback(IntPtr tagPtr, NativeError error);

        internal delegate void OnPrepareFailedDelegate(NativeError error);
        internal delegate void OnPrepareSuccessDelegate();
        internal delegate void OnScanSuccessDelegate(NativeArray barcodes, NativeSize inputSize, float inputRotation);
        internal delegate void OnScanFailedDelegate(NativeError error);

        #endregion

        #region Public callbacks

        public OnPrepareFailedDelegate  onPrepareFailedCallback;
        public OnPrepareSuccessDelegate  onPrepareSuccessCallback;
        public OnScanFailedDelegate  onScanFailedCallback;
        public OnScanSuccessDelegate  onScanSuccessCallback;

        #endregion

        #region Fields

        private IntPtr m_nativeHandle;

        #endregion

        #region Properties

        public IntPtr NativeHandle
        {
            get
            {
                return m_nativeHandle;
            }
            private set
            {
                m_nativeHandle = value;
            }
        }

        #endregion


        #region Life cycle

        public NativeGoogleMLKitBarcodeScannerListener()
        {
            m_nativeHandle = MarshalUtility.GetIntPtr(this);
        }

        ~NativeGoogleMLKitBarcodeScannerListener()
        {
            var tagHandle = GCHandle.FromIntPtr(m_nativeHandle);
            tagHandle.Free();
        }

        #endregion

        [MonoPInvokeCallback(typeof(BarcodeScannerPrepareSuccessNativeCallback))]
        public static void PrepareSuccessNativeCallback(IntPtr tag)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitBarcodeScannerListener listener = (NativeGoogleMLKitBarcodeScannerListener)tagHandle.Target;
            listener.onPrepareSuccessCallback();

        }


        [MonoPInvokeCallback(typeof(BarcodeScannerPrepareFailedNativeCallback))]
        public static void PrepareFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitBarcodeScannerListener listener = (NativeGoogleMLKitBarcodeScannerListener)tagHandle.Target;
            listener.onPrepareFailedCallback(error);
        }

        [MonoPInvokeCallback(typeof(BarcodeScannerScanSuccessNativeCallback))]
        public static void ScanSuccessNativeCallback(IntPtr tag, NativeArray barcodes, NativeSize inputSize, float inputRotation)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitBarcodeScannerListener listener = (NativeGoogleMLKitBarcodeScannerListener)tagHandle.Target;
            listener.onScanSuccessCallback(barcodes, inputSize, inputRotation);
        }

        [MonoPInvokeCallback(typeof(BarcodeScannerScanFailedNativeCallback))]
        public static void ScanFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitBarcodeScannerListener listener = (NativeGoogleMLKitBarcodeScannerListener)tagHandle.Target;
            listener.onScanFailedCallback(error);
        }
    }
}
#endif