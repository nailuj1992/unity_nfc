#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public class NativeArCoreCameraImageBuilder : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeArCoreCameraImageBuilder(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeArCoreCameraImageBuilder(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }

        public NativeArCoreCameraImageBuilder() : base(Native.kClassName)
        {
        }

#if NATIVE_PLUGINS_DEBUG
        ~NativeArCoreCameraImageBuilder()
        {
            DebugLogger.Log("Disposing NativeArCoreCameraImageBuilder");
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

        public NativeArCoreCameraImageBuilder AddPlane(int planeIndex, int rowStride, int pixelStride, NativeByteBuffer buffer)
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kAddPlane, planeIndex, rowStride, pixelStride, buffer.NativeObject);
            if(nativeObj != null)
            {
                NativeArCoreCameraImageBuilder data  = new  NativeArCoreCameraImageBuilder(nativeObj);
                return data;
            }
            else
            {
                return default(NativeArCoreCameraImageBuilder);
            }
        }
        public NativeArCoreCameraImage Build()
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kBuild);
            if(nativeObj != null)
            {
                NativeArCoreCameraImage data  = new  NativeArCoreCameraImage(nativeObj);
                return data;
            }
            else
            {
                return default(NativeArCoreCameraImage);
            }
        }
        public NativeArCoreCameraImageBuilder WithHeight(int height)
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kWithHeight, height);
            if(nativeObj != null)
            {
                NativeArCoreCameraImageBuilder data  = new  NativeArCoreCameraImageBuilder(nativeObj);
                return data;
            }
            else
            {
                return default(NativeArCoreCameraImageBuilder);
            }
        }
        public NativeArCoreCameraImageBuilder WithNumberOfPlanes(int planeCount)
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kWithNumberOfPlanes, planeCount);
            if(nativeObj != null)
            {
                NativeArCoreCameraImageBuilder data  = new  NativeArCoreCameraImageBuilder(nativeObj);
                return data;
            }
            else
            {
                return default(NativeArCoreCameraImageBuilder);
            }
        }
        public NativeArCoreCameraImageBuilder WithTimestamp(long timestamp)
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kWithTimestamp, timestamp);
            if(nativeObj != null)
            {
                NativeArCoreCameraImageBuilder data  = new  NativeArCoreCameraImageBuilder(nativeObj);
                return data;
            }
            else
            {
                return default(NativeArCoreCameraImageBuilder);
            }
        }
        public NativeArCoreCameraImageBuilder WithWidth(int width)
        {
            AndroidJavaObject nativeObj = Call<AndroidJavaObject>(Native.Method.kWithWidth, width);
            if(nativeObj != null)
            {
                NativeArCoreCameraImageBuilder data  = new  NativeArCoreCameraImageBuilder(nativeObj);
                return data;
            }
            else
            {
                return default(NativeArCoreCameraImageBuilder);
            }
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.types.ArCoreCameraImageBuilder";

            internal class Method
            {
                internal const string kAddPlane = "addPlane";
                internal const string kWithTimestamp = "withTimestamp";
                internal const string kWithNumberOfPlanes = "withNumberOfPlanes";
                internal const string kWithHeight = "withHeight";
                internal const string kWithWidth = "withWidth";
                internal const string kBuild = "build";
            }

        }
    }
}
#endif