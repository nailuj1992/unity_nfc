using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit.Internal
{
    public static class NativeFeatureType
    {
        #region Constants

        // grouped features
        public      const   string              kBarcodeScanner             = "BarcodeScanner";
        public      const   string              kObjectDetectorAndTracker   = "ObjectDetectorAndTracker";
        public      const   string              kTextRecognizer             = "TextRecognizer";
        public      const   string              kFaceDetector               = "FaceDetector";
        public      const   string              kDigitalInkRecognizer       = "DigitalInkRecognizer";

        #endregion
    }
}