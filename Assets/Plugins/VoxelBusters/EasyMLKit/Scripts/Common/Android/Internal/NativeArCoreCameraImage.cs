#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeArCoreCameraImage : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeArCoreCameraImage(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeArCoreCameraImage(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeArCoreCameraImage()
        {
            DebugLogger.Log("Disposing NativeArCoreCameraImage");
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

        public void Close()
        {
            Call(Native.Method.kClose);
        }
        public int GetFormat()
        {
            return Call<int>(Native.Method.kGetFormat);
        }
        public int GetHeight()
        {
            return Call<int>(Native.Method.kGetHeight);
        }
        public long GetTimestamp()
        {
            return Call<long>(Native.Method.kGetTimestamp);
        }
        public int GetWidth()
        {
            return Call<int>(Native.Method.kGetWidth);
        }
        public NativeByteBuffer ToNV21Format()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kToNV21Format);
            if(nativeObj != null)
            {
                NativeByteBuffer data  = new  NativeByteBuffer(nativeObj);
                return data;
            }
            else
            {
                return default(NativeByteBuffer);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.types.ArCoreCameraImageBuilder$ArCoreCameraImage";

            internal class Method
            {
                internal const string kGetWidth = "getWidth";
                internal const string kToNV21Format = "toNV21Format";
                internal const string kGetTimestamp = "getTimestamp";
                internal const string kGetFormat = "getFormat";
                internal const string kGetHeight = "getHeight";
                internal const string kClose = "close";
            }

        }
    }
}
#endif