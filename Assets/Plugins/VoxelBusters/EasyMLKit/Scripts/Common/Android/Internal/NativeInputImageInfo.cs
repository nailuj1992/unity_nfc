#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeInputImageInfo : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeInputImageInfo(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeInputImageInfo(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeInputImageInfo()
        {
            DebugLogger.Log("Disposing NativeInputImageInfo");
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

        public int GetHeight()
        {
            return Call<int>(Native.Method.kGetHeight);
        }
        public float GetRotation()
        {
            return Call<float>(Native.Method.kGetRotation);
        }
        public int GetWidth()
        {
            return Call<int>(Native.Method.kGetWidth);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.types.InputImageInfo";

            internal class Method
            {
                internal const string kGetWidth = "getWidth";
                internal const string kGetRotation = "getRotation";
                internal const string kGetHeight = "getHeight";
            }

        }
    }
}
#endif