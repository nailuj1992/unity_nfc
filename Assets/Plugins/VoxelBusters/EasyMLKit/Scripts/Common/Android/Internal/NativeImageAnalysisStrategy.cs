#if UNITY_ANDROID
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.NativePlugins.Android
{
    public enum NativeImageAnalysisStrategy
    {
        KeepOnlyLatest = 0,
        BlockProducer = 1
    }
    public class NativeImageAnalysisStrategyHelper
    {
        internal const string kClassName = "com.voxelbusters.mlkit.common.inputimage.types.ImageAnalysisStrategy";

        public static AndroidJavaObject CreateWithValue(NativeImageAnalysisStrategy value)
        {
#if NATIVE_PLUGINS_DEBUG
            DebugLogger.Log("[NativeImageAnalysisStrategyHelper : NativeImageAnalysisStrategyHelper][Method(CreateWithValue) : NativeImageAnalysisStrategy]");
#endif
            AndroidJavaClass javaClass = new AndroidJavaClass(kClassName);
            AndroidJavaObject[] values = javaClass.CallStatic<AndroidJavaObject[]>("values");
            return values[(int)value];
        }

        public static NativeImageAnalysisStrategy ReadFromValue(AndroidJavaObject value)
        {
            return (NativeImageAnalysisStrategy)value.Call<int>("ordinal");
        }
    }
}
#endif