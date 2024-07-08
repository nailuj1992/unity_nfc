#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeLiveCameraOptions : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeLiveCameraOptions(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeLiveCameraOptions(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeLiveCameraOptions() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeLiveCameraOptions()
        {
            DebugLogger.Log("Disposing NativeLiveCameraOptions");
        }
#endif
        #endregion
        #region Static methods
        private static AndroidJavaClass GetClass()
        {
            if (m_nativeClass == null)
            {
                m_nativeClass = new AndroidJavaClass(Native.kClassName);
            }
            return m_nativeClass;
        }

        #endregion
        #region Public methods

        public NativeImageAnalysisStrategy GetImageAnalysisStrategy()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetImageAnalysisStrategy);
            if(nativeObj != null)
            {
                NativeImageAnalysisStrategy data  = NativeImageAnalysisStrategyHelper.ReadFromValue(nativeObj);
                return data;
            }
            else
            {
                return default(NativeImageAnalysisStrategy);
            }
        }
        public bool GetUseFrontCam()
        {
            return Call<bool>(Native.Method.kGetUseFrontCam);
        }
        public bool IsFlashEnabled()
        {
            return Call<bool>(Native.Method.kIsFlashEnabled);
        }
        public bool IsPreviewEnabled()
        {
            return Call<bool>(Native.Method.kIsPreviewEnabled);
        }
        public void SetFlashEnabled(bool flashEnabled)
        {
            Call(Native.Method.kSetFlashEnabled, flashEnabled);
        }
        public void SetImageAnalysisStrategy(NativeImageAnalysisStrategy strategy)
        {
            Call(Native.Method.kSetImageAnalysisStrategy, NativeImageAnalysisStrategyHelper.CreateWithValue(strategy));
        }
        public void SetPreviewEnabled(bool previewEnabled)
        {
            Call(Native.Method.kSetPreviewEnabled, previewEnabled);
        }
        public void SetUseFrontCam(bool useFrontCam)
        {
            Call(Native.Method.kSetUseFrontCam, useFrontCam);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.types.LiveCameraOptions";

            internal class Method
            {
                internal const string kSetImageAnalysisStrategy = "setImageAnalysisStrategy";
                internal const string kGetImageAnalysisStrategy = "getImageAnalysisStrategy";
                internal const string kGetUseFrontCam = "getUseFrontCam";
                internal const string kIsFlashEnabled = "isFlashEnabled";
                internal const string kSetUseFrontCam = "setUseFrontCam";
                internal const string kSetFlashEnabled = "setFlashEnabled";
                internal const string kIsPreviewEnabled = "isPreviewEnabled";
                internal const string kSetPreviewEnabled = "setPreviewEnabled";
            }

        }
    }
}
#endif