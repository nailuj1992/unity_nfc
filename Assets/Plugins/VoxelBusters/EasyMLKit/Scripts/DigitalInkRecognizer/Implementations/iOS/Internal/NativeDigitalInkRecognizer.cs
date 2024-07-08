#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal class NativeDigitalInkRecognizer
    {
        #region Native Bindings

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_DigitalInkRecognizer_Init();

        [DllImport("__Internal")]
        public static extern void MLKit_DigitalInkRecognizer_Prepare(IntPtr digitalInkRecognizerPtr, ref NativeDrawing inputSourcePtr, ref NativeDigitalInkRecognizerOptions optionsPtr, ref NativeCallbackDataWrapper onSuccessCallback, ref NativeCallbackDataWrapper onFailureCallback);

        [DllImport("__Internal")]
        public static extern void MLKit_DigitalInkRecognizer_Process(IntPtr digitalInkRecognizerPtr, ref NativeCallbackDataWrapper onSuccessCallback, ref NativeCallbackDataWrapper onFailureCallback);

        [DllImport("__Internal")]
        public static extern void MLKit_DigitalInkRecognizer_Close(IntPtr digitalInkRecognizerPtr);

        [DllImport("__Internal")]
        public static extern IntPtr MLKit_DigitalInkRecognizer_GetModelManager(IntPtr digitalInkRecognizerPtr);


        #endregion

        #region Fields

        IntPtr m_nativeHandle;

        #endregion

        #region Constructor

        public NativeDigitalInkRecognizer()
        {
            m_nativeHandle = MLKit_DigitalInkRecognizer_Init();
        }

        #endregion

        #region Public methods

        public void Close()
        {
            MLKit_DigitalInkRecognizer_Close(m_nativeHandle);
        }

        public void Prepare(ref NativeDrawing inputSource, ref NativeDigitalInkRecognizerOptions options, NativeCallbackData onSuccess, NativeCallbackData<NativeError> onFailure)
        {
            NativeCallbackDataWrapper onSuccessCallback = onSuccess.GetDataWrapper();
            NativeCallbackDataWrapper onFailureCallback = onFailure.GetDataWrapper();

            MLKit_DigitalInkRecognizer_Prepare(m_nativeHandle, ref inputSource, ref options, ref onSuccessCallback, ref onFailureCallback);
        }

        public void Process(NativeCallbackData<NativeArray> onProcessUpdateSuccess, NativeCallbackData<NativeError> onProcessUpdateFailed)
        {
            NativeCallbackDataWrapper onSuccessCallback = onProcessUpdateSuccess.GetDataWrapper();
            NativeCallbackDataWrapper onFailureCallback = onProcessUpdateFailed.GetDataWrapper();

            MLKit_DigitalInkRecognizer_Process(m_nativeHandle, ref onSuccessCallback, ref onFailureCallback);
        }

        internal NativeDigitalInkRecognizerModelManager GetModelManager()
        {
            IntPtr nativeHandle = MLKit_DigitalInkRecognizer_GetModelManager(m_nativeHandle);
            return new NativeDigitalInkRecognizerModelManager(nativeHandle);
        }

        #endregion
    }
}
#endif