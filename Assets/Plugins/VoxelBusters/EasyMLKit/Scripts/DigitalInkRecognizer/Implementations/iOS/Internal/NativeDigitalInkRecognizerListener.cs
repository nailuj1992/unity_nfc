#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal class NativeDigitalInkRecognizerListener
    {
        #region Delegates

        internal delegate void DigitalInkRecognizerPrepareSuccessNativeCallback(IntPtr tagPtr);
        internal delegate void DigitalInkRecognizerPrepareFailedNativeCallback(IntPtr tagPtr, NativeError error);
        internal delegate void DigitalInkRecognizerScanSuccessNativeCallback(IntPtr tagPtr, NativeArray recognizedValues);
        internal delegate void DigitalInkRecognizerScanFailedNativeCallback(IntPtr tagPtr, NativeError error);

        internal delegate void OnPrepareFailedDelegate(NativeError error);
        internal delegate void OnPrepareSuccessDelegate();
        internal delegate void OnScanSuccessDelegate(NativeArray recognizedValues);
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

        public NativeDigitalInkRecognizerListener()
        {
            m_nativeHandle = MarshalUtility.GetIntPtr(this);
        }

        ~NativeDigitalInkRecognizerListener()
        {
            var tagHandle = GCHandle.FromIntPtr(m_nativeHandle);
            tagHandle.Free();
        }

        #endregion

        [MonoPInvokeCallback(typeof(DigitalInkRecognizerPrepareSuccessNativeCallback))]
        public static void PrepareSuccessNativeCallback(IntPtr tag)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerListener listener = (NativeDigitalInkRecognizerListener)tagHandle.Target;
            listener.onPrepareSuccessCallback();

        }


        [MonoPInvokeCallback(typeof(DigitalInkRecognizerPrepareFailedNativeCallback))]
        public static void PrepareFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerListener listener = (NativeDigitalInkRecognizerListener)tagHandle.Target;
            listener.onPrepareFailedCallback(error);
        }

        [MonoPInvokeCallback(typeof(DigitalInkRecognizerScanSuccessNativeCallback))]
        public static void ScanSuccessNativeCallback(IntPtr tag, NativeArray recognizedValues)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerListener listener = (NativeDigitalInkRecognizerListener)tagHandle.Target;
            listener.onScanSuccessCallback(recognizedValues);
        }

        [MonoPInvokeCallback(typeof(DigitalInkRecognizerScanFailedNativeCallback))]
        public static void ScanFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerListener listener = (NativeDigitalInkRecognizerListener)tagHandle.Target;
            listener.onScanFailedCallback(error);
        }
    }
}
#endif