using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit
{
    internal static class ImplementationBlueprint
    {
        #region Constants

        private     const   string      kMainAssembly                   = "VoxelBusters.EasyMLKit";
        
        private     const   string      kIOSAssembly                    = kMainAssembly;

        private     const   string      kAndroidAssembly                = kMainAssembly;

        private     const   string      kSimulatorAssembly              = kMainAssembly;

        private     const   string      kRootNamespace                  = "VoxelBusters.EasyMLKit";

        #endregion

        #region Static properties

        public static NativeFeatureRuntimeConfiguration BarcodeScanner 
        { 
            get { return GetBarcodeScannerConfig(); } 
        }

        public static NativeFeatureRuntimeConfiguration ObjectDetectorAndTracker
        {
            get { return GetObjectDetectorAndTrackerConfig(); }
        }

        public static NativeFeatureRuntimeConfiguration TextRecognizer
        {
            get { return GetTextRecognizerConfig(); }
        }

        public static NativeFeatureRuntimeConfiguration FaceDetector
        {
            get { return GetFaceDetectorConfig(); }
        }

        public static NativeFeatureRuntimeConfiguration DigitalInkRecognizer
        {
            get { return GetDigitalInkRecognizerConfig(); }
        }

        #endregion

        #region Constructors

        private static NativeFeatureRuntimeConfiguration GetBarcodeScannerConfig()
        {
            return GetConfig(NativeFeatureType.kBarcodeScanner);
        }

        private static NativeFeatureRuntimeConfiguration GetObjectDetectorAndTrackerConfig()
        {
            return GetConfig(NativeFeatureType.kObjectDetectorAndTracker);
        }

        private static NativeFeatureRuntimeConfiguration GetTextRecognizerConfig()
        {
            return GetConfig(NativeFeatureType.kTextRecognizer);
        }


        private static NativeFeatureRuntimeConfiguration GetFaceDetectorConfig()
        {
            return GetConfig(NativeFeatureType.kFaceDetector);
        }


        private static NativeFeatureRuntimeConfiguration GetDigitalInkRecognizerConfig()
        {
            return GetConfig(NativeFeatureType.kDigitalInkRecognizer);
        }


        private static NativeFeatureRuntimeConfiguration GetConfig(string featureName)
        {
            string featureNamespace = $"{kRootNamespace}";
            string nativeInterfaceType = featureName + "Implementation";
            return new NativeFeatureRuntimeConfiguration(
                packages: new NativeFeatureRuntimePackage[]
                {
                    new NativeFeatureRuntimePackage(platform: RuntimePlatform.IPhonePlayer, assembly: kIOSAssembly, ns: featureNamespace + ".Implementations.iOS", nativeInterfaceType: nativeInterfaceType),
                    new NativeFeatureRuntimePackage(platform: RuntimePlatform.Android, assembly: kAndroidAssembly, ns: featureNamespace + ".Implementations.Android", nativeInterfaceType: nativeInterfaceType),
                },
                simulatorPackage: new NativeFeatureRuntimePackage(assembly: kSimulatorAssembly, ns: featureNamespace + ".Implementations.Simulator", nativeInterfaceType: nativeInterfaceType),
                fallbackPackage: new NativeFeatureRuntimePackage(assembly: kMainAssembly, ns: featureNamespace + ".Implementations.Null", nativeInterfaceType: nativeInterfaceType));
        }

        #endregion

        #region Public static methods

        public static NativeFeatureRuntimeConfiguration GetRuntimeConfiguration(string featureName)
        {
            switch (featureName)
            {
                case NativeFeatureType.kBarcodeScanner:
                    return BarcodeScanner;

                case NativeFeatureType.kObjectDetectorAndTracker:
                    return ObjectDetectorAndTracker;

                case NativeFeatureType.kTextRecognizer:
                    return TextRecognizer;

                case NativeFeatureType.kFaceDetector:
                    return FaceDetector;

                default:
                    DebugLogger.LogError("No runtime configuration found for feature : " + featureName);
                    return null;
            }
        }

        #endregion
    }
}