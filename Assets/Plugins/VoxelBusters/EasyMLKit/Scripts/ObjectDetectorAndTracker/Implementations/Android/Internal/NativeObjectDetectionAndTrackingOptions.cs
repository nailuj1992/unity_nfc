#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeObjectDetectionAndTrackingOptions : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeObjectDetectionAndTrackingOptions(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeObjectDetectionAndTrackingOptions(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeObjectDetectionAndTrackingOptions() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeObjectDetectionAndTrackingOptions()
        {
            DebugLogger.Log("Disposing NativeObjectDetectionAndTrackingOptions");
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

        public float GetClassificationConfidenceThreshold()
        {
            return Call<float>(Native.Method.kGetClassificationConfidenceThreshold);
        }
        public int GetMaxPerObjectLabelCount()
        {
            return Call<int>(Native.Method.kGetMaxPerObjectLabelCount);
        }
        public string GetModelPath()
        {
            return Call<string>(Native.Method.kGetModelPath);
        }
        public bool IsEnableClassification()
        {
            return Call<bool>(Native.Method.kIsEnableClassification);
        }
        public bool IsEnableMultiObjectDetection()
        {
            return Call<bool>(Native.Method.kIsEnableMultiObjectDetection);
        }
        public void SetClassificationConfidenceThreshold(float classificationConfidenceThreshold)
        {
            Call(Native.Method.kSetClassificationConfidenceThreshold, classificationConfidenceThreshold);
        }
        public void SetEnableClassification(bool enableClassification)
        {
            Call(Native.Method.kSetEnableClassification, enableClassification);
        }
        public void SetEnableMultiObjectDetection(bool enableMultiObjectDetection)
        {
            Call(Native.Method.kSetEnableMultiObjectDetection, enableMultiObjectDetection);
        }
        public void SetModelPath(string modelPath)
        {
            Call(Native.Method.kSetModelPath, modelPath);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.objectdetectionandtracking.googlemlkit.types.ObjectDetectionAndTrackingOptions";

            internal class Method
            {
                internal const string kSetModelPath = "setModelPath";
                internal const string kGetModelPath = "getModelPath";
                internal const string kIsEnableClassification = "isEnableClassification";
                internal const string kGetClassificationConfidenceThreshold = "getClassificationConfidenceThreshold";
                internal const string kSetClassificationConfidenceThreshold = "setClassificationConfidenceThreshold";
                internal const string kGetMaxPerObjectLabelCount = "getMaxPerObjectLabelCount";
                internal const string kSetEnableClassification = "setEnableClassification";
                internal const string kSetEnableMultiObjectDetection = "setEnableMultiObjectDetection";
                internal const string kIsEnableMultiObjectDetection = "isEnableMultiObjectDetection";
            }

        }
    }
}
#endif