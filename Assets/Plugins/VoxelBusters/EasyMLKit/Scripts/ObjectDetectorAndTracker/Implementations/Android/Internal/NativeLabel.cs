#if UNITY_ANDROID
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android.Internal
{
    public class NativeLabel : NativeAndroidJavaObjectWrapper
    {
        #region Static properties

         private static AndroidJavaClass m_nativeClass;

        #endregion

        #region Constructor

        // Wrapper constructors
        public NativeLabel(AndroidJavaObject androidJavaObject) : base(Native.kClassName, androidJavaObject)
        {
        }

        public NativeLabel(NativeAndroidJavaObjectWrapper wrapper) : base(wrapper)
        {
        }


#if NATIVE_PLUGINS_DEBUG
        ~NativeLabel()
        {
            DebugLogger.Log("Disposing NativeLabel");
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

        public bool Equals(NativeObject arg0)
        {
            return Call<bool>(Native.Method.kEquals, arg0.NativeObject);
        }
        public float GetConfidence()
        {
            return Call<float>(Native.Method.kGetConfidence);
        }
        public int GetIndex()
        {
            return Call<int>(Native.Method.kGetIndex);
        }
        public string GetText()
        {
            return Call<string>(Native.Method.kGetText);
        }
        public int HashCode()
        {
            return Call<int>(Native.Method.kHashCode);
        }

        #endregion

        internal class Native
        {
            internal const string kClassName = "com.google.mlkit.vision.objects.DetectedObject$Label";

            internal class Method
            {
                internal const string kGetIndex = "getIndex";
                internal const string kHashCode = "hashCode";
                internal const string kGetConfidence = "getConfidence";
                internal const string kGetText = "getText";
                internal const string kEquals = "equals";
            }

        }
    }
}
#endif