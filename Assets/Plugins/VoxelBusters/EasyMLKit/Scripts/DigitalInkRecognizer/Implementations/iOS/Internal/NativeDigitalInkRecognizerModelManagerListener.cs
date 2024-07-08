#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal class NativeDigitalInkRecognizerModelManagerListener
    {
        #region Delegates

        internal delegate void DigitalInkRecognizerModelManagerDownloadSuccessNativeCallback(IntPtr tagPtr);
        internal delegate void DigitalInkRecognizerModelManagerDownloadFailedNativeCallback(IntPtr tagPtr, NativeError error);
        internal delegate void DigitalInkRecognizerModelManagerDeleteSuccessNativeCallback(IntPtr tagPtr);
        internal delegate void DigitalInkRecognizerModelManagerDeleteFailedNativeCallback(IntPtr tagPtr, NativeError error);

        internal delegate void OnDownloadFailedDelegate(NativeError error);
        internal delegate void OnDownloadSuccessDelegate();
        internal delegate void OnDeleteSuccessDelegate();
        internal delegate void OnDeleteFailedDelegate(NativeError error);

        #endregion

        #region Public callbacks

        public OnDownloadFailedDelegate  onDownloadFailedCallback;
        public OnDownloadSuccessDelegate  onDownloadSuccessCallback;
        public OnDeleteFailedDelegate  onDeleteFailedCallback;
        public OnDeleteSuccessDelegate  onDeleteSuccessCallback;

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

        public NativeDigitalInkRecognizerModelManagerListener()
        {
            m_nativeHandle = MarshalUtility.GetIntPtr(this);
        }

        ~NativeDigitalInkRecognizerModelManagerListener()
        {
            var tagHandle = GCHandle.FromIntPtr(m_nativeHandle);
            tagHandle.Free();
        }

        #endregion

        [MonoPInvokeCallback(typeof(DigitalInkRecognizerModelManagerDownloadSuccessNativeCallback))]
        public static void DownloadSuccessNativeCallback(IntPtr tag)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerModelManagerListener listener = (NativeDigitalInkRecognizerModelManagerListener)tagHandle.Target;
            listener.onDownloadSuccessCallback();

        }


        [MonoPInvokeCallback(typeof(DigitalInkRecognizerModelManagerDownloadFailedNativeCallback))]
        public static void DownloadFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerModelManagerListener listener = (NativeDigitalInkRecognizerModelManagerListener)tagHandle.Target;
            listener.onDownloadFailedCallback(error);
        }

        [MonoPInvokeCallback(typeof(DigitalInkRecognizerModelManagerDeleteSuccessNativeCallback))]
        public static void DeleteSuccessNativeCallback(IntPtr tag)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerModelManagerListener listener = (NativeDigitalInkRecognizerModelManagerListener)tagHandle.Target;
            listener.onDeleteSuccessCallback();
        }

        [MonoPInvokeCallback(typeof(DigitalInkRecognizerModelManagerDeleteFailedNativeCallback))]
        public static void DeleteFailedNativeCallback(IntPtr tag, NativeError error)
        {
            var tagHandle = GCHandle.FromIntPtr(tag);

            NativeDigitalInkRecognizerModelManagerListener listener = (NativeDigitalInkRecognizerModelManagerListener)tagHandle.Target;
            listener.onDeleteFailedCallback(error);
        }
    }
}
#endif