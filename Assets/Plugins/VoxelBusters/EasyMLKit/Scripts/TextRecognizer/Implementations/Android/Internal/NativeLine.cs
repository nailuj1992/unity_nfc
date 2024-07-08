#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeLine : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeLine(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeLine(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeLine()
        {
            DebugLogger.Log("Disposing NativeLine");
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

        public NativeRect GetBoundingBox()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetBoundingBox);
            if(nativeObj != null)
            {
                NativeRect data  = new  NativeRect(nativeObj);
                return data;
            }
            else
            {
                return default(NativeRect);
            }
        }
        public NativePoint[] GetCornerPoints()
        {
            AndroidJavaObject[] nativeObjs = Call<AndroidJavaObject[]>(Native.Method.kGetCornerPoints);
            NativePoint[] data  = new  NativePoint[nativeObjs.Length];
            for(int i=0; i<nativeObjs.Length; i++)
            {
                if(nativeObjs[i] != null)
                {
                    data[i] = new NativePoint(nativeObjs[i]);
                }
                else
                {
                    data[i] = null;
                }
            }
            return data;
        }
        public NativeList<NativeElement> GetElements()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetElements);
            if(nativeObj != null)
            {
                NativeList<NativeElement> data  = new  NativeList<NativeElement>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeElement>);
            }
        }
        public string GetRecognizedLanguage()
        {
            return Call<string>(Native.Method.kGetRecognizedLanguage);
        }
        public string GetText()
        {
            return Call<string>(Native.Method.kGetText);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.text.Text$Line";

            internal class Method
            {
                internal const string kGetElements = "getElements";
                internal const string kGetText = "getText";
                internal const string kGetRecognizedLanguage = "getRecognizedLanguage";
                internal const string kGetCornerPoints = "getCornerPoints";
                internal const string kGetBoundingBox = "getBoundingBox";
            }

        }
    }
}
#endif