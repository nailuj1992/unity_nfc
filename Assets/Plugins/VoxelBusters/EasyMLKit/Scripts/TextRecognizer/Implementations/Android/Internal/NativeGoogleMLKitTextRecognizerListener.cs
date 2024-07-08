#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeGoogleMLKitTextRecognizerListener : AndroidJavaProxy
    {
        #region Delegates

        public delegate void OnPrepareFailedDelegate(NativeException exception);
        public delegate void OnPrepareSuccessDelegate();
        public delegate void OnScanFailedDelegate(NativeException exception);
        public delegate void OnScanSuccessDelegate(NativeText text, NativeInputImageInfo dimensions);

        #endregion

        #region Public callbacks

        public OnPrepareFailedDelegate  onPrepareFailedCallback;
        public OnPrepareSuccessDelegate  onPrepareSuccessCallback;
        public OnScanFailedDelegate  onScanFailedCallback;
        public OnScanSuccessDelegate  onScanSuccessCallback;

        #endregion


        #region Constructors

        public NativeGoogleMLKitTextRecognizerListener() : base("com.voxelbusters.mlkit.textrecognitionv2.googlemlkit.IGoogleMLKitTextRecognizerListener")
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
        public void onScanFailed(AndroidJavaObject exception)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onScanFailed"  + " " + "[" + "exception" + " : " + exception +"]");
#endif
            if(onScanFailedCallback != null)
            {
                onScanFailedCallback(new NativeException(exception));
            }
        }
        public void onScanSuccess(AndroidJavaObject text, AndroidJavaObject dimensions)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onScanSuccess"  + " " + "[" + "text" + " : " + text +"]" + " " + "[" + "dimensions" + " : " + dimensions +"]");
#endif
            if(onScanSuccessCallback != null)
            {
                onScanSuccessCallback(new NativeText(text), new NativeInputImageInfo(dimensions));
            }
        }

        #endregion
    }
}
#endif