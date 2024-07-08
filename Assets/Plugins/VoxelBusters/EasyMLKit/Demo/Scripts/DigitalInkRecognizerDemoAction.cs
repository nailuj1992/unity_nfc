using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;

namespace VoxelBusters.EasyMLKit.Demo
{
    public enum DigitalInkRecognizerDemoActionType
    {
        CheckModelAvailability,
        DownloadModel,
        DeleteModel,
        ScanDrawingFromCanvas,
        ResourcePage,
    }

    public class DigitalInkRecognizerDemoAction : DemoActionBehaviour<DigitalInkRecognizerDemoActionType>
    { }
}