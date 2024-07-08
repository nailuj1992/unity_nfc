using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit.Internal
{
    public static class ProductResources
    {
        #region Constants

        private     const   string      kLiteVersionProductPage                 = "https://bit.ly/easy-ml-kit-unity";
        
        private     const   string      kFullVersionProductPage                 = "https://bit.ly/easy-ml-kit-unity";

        private     const   string      kPublisherPage                          = "http://bit.ly/ustoreVB";

        private     const   string      kDocumentationPage                      = "https://assetstore.easymlkit.voxelbusters.com/tutorials";

        private     const   string      kForumPage                              = "https://bit.ly/easy-ml-kit-unity-forum";

        private     const   string      kTutorialPage                           = "https://assetstore.easymlkit.voxelbusters.com/tutorials";

        private     const   string      kDiscordPage                            = "https://discord.gg/y4kQAefbJ8";

        private     const   string      kBarcodeScannerPage                     = "https://assetstore.easymlkit.voxelbusters.com/tutorials/features/barcode-scanner";
        private     const   string      kObjectDetectorAndTrackerPage           = "https://assetstore.easymlkit.voxelbusters.com/tutorials/features/object-detector-and-tracker";
        private     const   string      kTextRecognizerPage                     = "https://assetstore.easymlkit.voxelbusters.com/tutorials/features/text-recognizer";
        private     const   string      kDigitalInkRecognizerPage               = "https://assetstore.easymlkit.voxelbusters.com/tutorials/features/digital-ink-recognizer";


        #endregion

        #region Public static methods

        public static void OpenAssetStorePage(bool fullVersion)
        {
            Application.OpenURL(fullVersion ? kFullVersionProductPage : kLiteVersionProductPage);
        }

        public static void OpenPublisherPage()
        {
            Application.OpenURL(kPublisherPage);
        }

        public static void OpenDocumentation()
        {
            Application.OpenURL(kDocumentationPage);
        }

        public static void OpenForum()
        {
            Application.OpenURL(kForumPage);
        }

        public static void OpenTutorials()
        {
            Application.OpenURL(kTutorialPage);
        }

        public static void OpenSupport()
        {
            Application.OpenURL(kDiscordPage);
        }

        public static void OpenResourcePage(string feature)
        {
            string resourcePage = null;
            switch (feature)
            {
                case NativeFeatureType.kBarcodeScanner:
                    resourcePage = kBarcodeScannerPage;
                    break;

                case NativeFeatureType.kObjectDetectorAndTracker:
                    resourcePage = kObjectDetectorAndTrackerPage;
                    break;

                case NativeFeatureType.kTextRecognizer:
                    resourcePage = kTextRecognizerPage;
                    break;

                case NativeFeatureType.kDigitalInkRecognizer:
                    resourcePage = kDigitalInkRecognizerPage;
                    break;
            }

            // open link
            if (resourcePage != null)
            {
                Application.OpenURL(resourcePage);
            }
        }

        #endregion
    }
}