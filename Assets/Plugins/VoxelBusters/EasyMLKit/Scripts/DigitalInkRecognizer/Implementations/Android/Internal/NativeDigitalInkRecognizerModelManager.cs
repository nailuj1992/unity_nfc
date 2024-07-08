#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizerModelManager : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkRecognizerModelManager(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkRecognizerModelManager(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeDigitalInkRecognizerModelManager() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkRecognizerModelManager()
        {
            DebugLogger.Log("Disposing NativeDigitalInkRecognizerModelManager");
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

        public void DeleteModel(NativeDigitalInkRecognizerModelIdentifier modelIdentifier, NativeModelManagerActionListener listener)
        {
            Call(Native.Method.kDeleteModel, modelIdentifier.NativeObject, listener);
        }
        public void DownloadModel(NativeDigitalInkRecognizerModelIdentifier modelIdentifier, NativeModelManagerActionListener listener)
        {
            Call(Native.Method.kDownloadModel, modelIdentifier.NativeObject, listener);
        }
        public bool IsModelAvailable(NativeDigitalInkRecognizerModelIdentifier modelIdentifier)
        {
            return Call<bool>(Native.Method.kIsModelAvailable, modelIdentifier.NativeObject);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkRecognizerModelManager";

            internal class Method
            {
                internal const string kDownloadModel = "downloadModel";
                internal const string kDeleteModel = "deleteModel";
                internal const string kIsModelAvailable = "isModelAvailable";
            }

        }
    }
}
#endif