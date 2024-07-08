#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    internal class NativeDigitalInkRecognizerModelManager
    {
        #region Native Bindings

        [DllImport("__Internal")]
        public static extern bool MLKit_DigitalInkModelManager_IsModelAvailable(IntPtr nativeHandle, ref NativeDigitalInkRecognizerModelIdentifier modelIdentifier);

        [DllImport("__Internal")]
        public static extern void MLKit_DigitalInkModelManager_DownloadModel(IntPtr nativeHandle, ref NativeDigitalInkRecognizerModelIdentifier modelIdentifier, ref NativeCallbackDataWrapper successWrapper, ref NativeCallbackDataWrapper failureWrapper);

        [DllImport("__Internal")]
        public static extern void MLKit_DigitalInkModelManager_DeleteModel(IntPtr nativeHandle, ref NativeDigitalInkRecognizerModelIdentifier modelIdentifier, ref NativeCallbackDataWrapper successWrapper, ref NativeCallbackDataWrapper failureWrapper);

        #endregion

        #region Fields

        IntPtr m_nativeHandle;

        #endregion

        #region Constructor

        public NativeDigitalInkRecognizerModelManager(IntPtr nativeHandle)
        {
            m_nativeHandle = nativeHandle;
        }


        ~NativeDigitalInkRecognizerModelManager()
        {
            Debug.LogError("Deallocating.....");
        }

        #endregion

        #region Public methods

        public bool IsModelAvailable(NativeDigitalInkRecognizerModelIdentifier modelIdentifier)
        {
            return MLKit_DigitalInkModelManager_IsModelAvailable(m_nativeHandle, ref modelIdentifier);
        }

        public void DownloadModel(NativeDigitalInkRecognizerModelIdentifier modelIdentifier, NativeCallbackData onSuccess, NativeCallbackData<NativeError> onFailure)
        {
            NativeCallbackDataWrapper successWrapper = onSuccess.GetDataWrapper();
            NativeCallbackDataWrapper failureWrapper = onFailure.GetDataWrapper();

            MLKit_DigitalInkModelManager_DownloadModel(m_nativeHandle, ref modelIdentifier, ref successWrapper, ref failureWrapper);
        }


        public void DeleteModel(NativeDigitalInkRecognizerModelIdentifier modelIdentifier, NativeCallbackData onSuccess, NativeCallbackData<NativeError> onFailure)
        {
            NativeCallbackDataWrapper successWrapper = onSuccess.GetDataWrapper();
            NativeCallbackDataWrapper failureWrapper = onFailure.GetDataWrapper();

            MLKit_DigitalInkModelManager_DeleteModel(m_nativeHandle, ref modelIdentifier, ref successWrapper, ref failureWrapper);

            
        }

        #endregion
    }
}
#endif