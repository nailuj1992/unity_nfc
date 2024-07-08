using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;

namespace VoxelBusters.EasyMLKit.Demo
{
    public enum TextRecognizerDemoActionType
    {
        ScanTextFromImage,
        ScanTextFromLiveCamera,
        ScanTextFromARCamera,
        ResourcePage,
    }

    public class TextRecognizerDemoAction : DemoActionBehaviour<TextRecognizerDemoActionType>
    { }
}