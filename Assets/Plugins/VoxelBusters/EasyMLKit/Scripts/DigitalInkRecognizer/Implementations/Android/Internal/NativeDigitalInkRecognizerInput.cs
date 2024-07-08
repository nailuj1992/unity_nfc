#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeDigitalInkRecognizerInput : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeDigitalInkRecognizerInput(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeDigitalInkRecognizerInput(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeDigitalInkRecognizerInput() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeDigitalInkRecognizerInput()
        {
            DebugLogger.Log("Disposing NativeDigitalInkRecognizerInput");
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

        public void AddStroke(NativeDigitalInkStroke stroke)
        {
            Call(Native.Method.kAddStroke, stroke.NativeObject);
        }
        public NativeList<NativeDigitalInkStroke> GetStrokes()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetStrokes);
            if(nativeObj != null)
            {
                NativeList<NativeDigitalInkStroke> data  = new  NativeList<NativeDigitalInkStroke>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeDigitalInkStroke>);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.digitalinkrecognition.DigitalInkRecognizerInput";

            internal class Method
            {
                internal const string kAddStroke = "addStroke";
                internal const string kGetStrokes = "getStrokes";
            }

        }
    }
}
#endif