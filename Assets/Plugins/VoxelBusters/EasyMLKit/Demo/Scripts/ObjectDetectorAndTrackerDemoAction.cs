using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;

namespace VoxelBusters.EasyMLKit.Demo
{
    public enum ObjectDetectorAndTrackerDemoType
    {
        DetectAndTrackFromImage,
        DetectAndTrackFromLiveCamera,
        DetectAndTrackFromARFoundationCamera,
        ResourcePage,
    }

    public class ObjectDetectorAndTrackerDemoAction : DemoActionBehaviour<ObjectDetectorAndTrackerDemoType>
    { }
}