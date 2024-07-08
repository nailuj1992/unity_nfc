using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;

namespace VoxelBusters.EasyMLKit.Demo
{
    public enum BarcodeScannerDemoActionType
    {
        ScanBarcodeFromLiveCamera,
        ScanBarcodeFromImage,
        ScanBarcodeFromARCamera,
        ResourcePage,
    }

    public class BarcodeScannerDemoAction : DemoActionBehaviour<BarcodeScannerDemoActionType>
    { }
}