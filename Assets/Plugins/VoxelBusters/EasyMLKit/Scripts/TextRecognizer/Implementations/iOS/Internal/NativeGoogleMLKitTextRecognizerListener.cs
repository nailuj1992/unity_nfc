#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal class NativeGoogleMLKitTextRecognizerListener
    {
        #region Delegates

        internal delegate void TextRecognizerPrepareSuccessNativeCallback(IntPtr tagPtr);
        internal delegate void TextRecognizerPrepareFailedNativeCallback(IntPtr tagPtr, NativeError error);
        internal delegate void TextRecognizerScanSuccessNativeCallback(IntPtr tagPtr, NativeText text, NativeSize inputSize, float inputRotation);
        internal delegate void TextRecognizerScanFailedNativeCallback(IntPtr tagPtr, NativeError error);

        internal delegate void OnPrepareFailedDelegate(NativeError error);
        internal delegate void OnPrepareSuccessDelegate();
        internal delegate void OnScanSuccessDelegate(NativeText text, NativeSize inputSize, float inputRotation);
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

        public NativeGoogleMLKitTextRecognizerListener()
        {
            m_nativeHandle = MarshalUtility.GetIntPtr(this);
        }

        ~NativeGoogleMLKitTextRecognizerListener()
        {
            var tagHandle = GCHandle.FromIntPtr(m_nativeHandle);
            tagHandle.Free();
        }

        #endregion

        [MonoPInvokeCallback(typeof(TextRecognizerPrepareSuccessNativeCallback))]
        public static void PrepareSuccessNativeCallback(IntPtr tag)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitTextRecognizerListener listener = (NativeGoogleMLKitTextRecognizerListener)tagHandle.Target;
            listener.onPrepareSuccessCallback();

        }


        [MonoPInvokeCallback(typeof(TextRecognizerPrepareFailedNativeCallback))]
        public static void PrepareFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitTextRecognizerListener listener = (NativeGoogleMLKitTextRecognizerListener)tagHandle.Target;
            listener.onPrepareFailedCallback(error);
        }

        [MonoPInvokeCallback(typeof(TextRecognizerScanSuccessNativeCallback))]
        public static void ScanSuccessNativeCallback(IntPtr tag, NativeText text, NativeSize inputSize, float inputRotation)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitTextRecognizerListener listener = (NativeGoogleMLKitTextRecognizerListener)tagHandle.Target;
            listener.onScanSuccessCallback(text, inputSize, inputRotation);
        }

        [MonoPInvokeCallback(typeof(TextRecognizerScanFailedNativeCallback))]
        public static void ScanFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeGoogleMLKitTextRecognizerListener listener = (NativeGoogleMLKitTextRecognizerListener)tagHandle.Target;
            listener.onScanFailedCallback(error);
        }
    }
}
#endif