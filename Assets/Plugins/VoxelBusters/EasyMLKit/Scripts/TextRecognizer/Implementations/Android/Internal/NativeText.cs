#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeText : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeText(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeText(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeText()
        {
            DebugLogger.Log("Disposing NativeText");
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

        public string GetText()
        {
            return Call<string>(Native.Method.kGetText);
        }
        public NativeList<NativeTextBlock> GetTextBlocks()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kGetTextBlocks);
            if(nativeObj != null)
            {
                NativeList<NativeTextBlock> data  = new  NativeList<NativeTextBlock>(nativeObj);
                return data;
            }
            else
            {
                return default(NativeList<NativeTextBlock>);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.text.Text";

            internal class Method
            {
                internal const string kGetTextBlocks = "getTextBlocks";
                internal const string kGetText = "getText";
            }

        }
    }
}
#endif