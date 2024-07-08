#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizerListener : AndroidJavaProxy
    {
        #region Delegates

        public delegate void OnPrepareFailedDelegate(NativeException exception);
        public delegate void OnPrepareSuccessDelegate();
        public delegate void OnScanFailedDelegate(NativeException exception);
        public delegate void OnScanSuccessDelegate(NativeList<NativeDigitalInkRecognizedValue> recognizedValues);

        #endregion

        #region Public callbacks

        public OnPrepareFailedDelegate  onPrepareFailedCallback;
        public OnPrepareSuccessDelegate  onPrepareSuccessCallback;
        public OnScanFailedDelegate  onScanFailedCallback;
        public OnScanSuccessDelegate  onScanSuccessCallback;

        #endregion


        #region Constructors

        public NativeDigitalInkRecognizerListener() : base("com.voxelbusters.mlkit.digitalinkrecognition.interfaces.IDigitalInkRecognizerListener")
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
        public void onScanSuccess(AndroidJavaObject recognizedValues)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[Proxy : Callback] : " + "onScanSuccess"  + " " + "[" + "recognizedValues" + " : " + recognizedValues +"]");
#endif
            if(onScanSuccessCallback != null)
            {
                onScanSuccessCallback(new NativeList<NativeDigitalInkRecognizedValue>(recognizedValues));
            }
        }

        #endregion
    }
}
#endif