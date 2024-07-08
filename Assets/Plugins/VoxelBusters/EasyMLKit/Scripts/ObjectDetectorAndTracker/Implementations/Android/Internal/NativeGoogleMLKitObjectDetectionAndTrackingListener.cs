#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeGoogleMLKitObjectDetectionAndTrackingListener : AndroidJavaProxy
    {
        #region Delegates

        public delegate void OnDetectionFailedDelegate(NativeException exception);
        public delegate void OnDetectionSuccessDelegate(NativeList<NativeDetectedObject> detectedObjects, NativeInputImageInfo inputImageInfo);
        public delegate void OnPrepareFailedDelegate(NativeException exception);
        public delegate void OnPrepareSuccessDelegate();

        #endregion

        #region Public callbacks

        public OnDetectionFailedDelegate  onDetectionFailedCallback;
        public OnDetectionSuccessDelegate  onDetectionSuccessCallback;
        public OnPrepareFailedDelegate  onPrepareFailedCallback;
        public OnPrepareSuccessDelegate  onPrepareSuccessCallback;

        #endregion


        #region Constructors

        public NativeGoogleMLKitObjectDetectionAndTrackingListener() : base("com.voxelbusters.mlkit.objectdetectionandtracking.googlemlkit.IGoogleMLKitObjectDetectionAndTrackingListener")
        {
        }

        #endregion


        #region Public methods
#if NATIVE_PLUGINS_DEBUG
        public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
        {
            DebugLogger.Log("**************************************************");
            DebugLogger.Log("[Generic Invoke : " +  methodName + "]" + " Args Length : " + (javaArgs != null ? javaArgs.Length : 0));
            if(javaArgs != null)
            {
                System.Text.StringBuilder builder = new System.Text.StringBuilder();

                foreach(AndroidJavaObject each in javaArgs)
                {
                    if(each != null)
                    {
                        builder.Append(string.Format("[Type : {0} Value : {1}]", each.Call<AndroidJavaObject>("getClass").Call<string>("getName"), each.Call<string>("toString")));
                        builder.Append("\n");
                    }
                    else
                    {
                        builder.Append("[Value : null]");
                        builder.Append("\n");
                    }
                }

                DebugLogger.Log(builder.ToString());
            }
            DebugLogger.Log("-----------------------------------------------------");
            return base.Invoke(methodName, javaArgs);
        }
#endif

        public void onDetectionFailed(AndroidJavaObject exception)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onDetectionFailed"  + " " + "[" + "exception" + " : " + exception +"]");
#endif
            if(onDetectionFailedCallback != null)
            {
                onDetectionFailedCallback(new NativeException(exception));
            }
        }
        public void onDetectionSuccess(AndroidJavaObject detectedObjects, AndroidJavaObject inputImageInfo)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onDetectionSuccess"  + " " + "[" + "detectedObjects" + " : " + detectedObjects +"]" + " " + "[" + "inputImageInfo" + " : " + inputImageInfo +"]");
#endif
            if(onDetectionSuccessCallback != null)
            {
                onDetectionSuccessCallback(new NativeList<NativeDetectedObject>(detectedObjects), new NativeInputImageInfo(inputImageInfo));
            }
        }
        public void onPrepareFailed(AndroidJavaObject exception)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onPrepareFailed"  + " " + "[" + "exception" + " : " + exception +"]");
#endif
            if(onPrepareFailedCallback != null)
            {
                onPrepareFailedCallback(new NativeException(exception));
            }
        }
        public void onPrepareSuccess()
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onPrepareSuccess" );
#endif
            if(onPrepareSuccessCallback != null)
            {
                onPrepareSuccessCallback();
            }
        }

        #endregion
    }
}
#endif