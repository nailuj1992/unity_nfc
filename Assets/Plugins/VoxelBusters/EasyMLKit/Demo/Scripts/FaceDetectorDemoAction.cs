using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;

namespace VoxelBusters.EasyMLKit.Demo
{
    public enum FaceDetectorDemoActionType
    {
        ScanFromImage,
        ScanFromLiveCamera,
        ScanFromARCamera,
        ResourcePage,
    }

    public class FaceDetectorDemoAction : DemoActionBehaviour<FaceDetectorDemoActionType>
    { }
}