using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit
{
    public class EasyMLKitSettings : ScriptableObject
    {
        #region Static fields

        private     static EasyMLKitSettings s_sharedInstance                = null;

        #endregion

        #region Fields

        [SerializeField]
        private BarcodeScannerUnitySettings m_barcodeScannerSettings           = new BarcodeScannerUnitySettings();

        [SerializeField]
        private ObjectDetectorAndTrackerUnitySettings m_objectDetectorAndTrackerSettings = new ObjectDetectorAndTrackerUnitySettings();

        [SerializeField]
        private TextRecognizerUnitySettings m_textRecognizerSettings = new TextRecognizerUnitySettings();

        [SerializeField]
        private DigitalInkRecognizerUnitySettings m_digitalInkRecognizerSettings = new DigitalInkRecognizerUnitySettings();

        [SerializeField]
        private FaceDetectorUnitySettings m_faceDetectorSettings = new FaceDetectorUnitySettings();


        #endregion

        #region Static properties

        public static string PackageName { get { return "com.voxelbusters.easymlkit"; } }

        public static string DisplayName { get { return "Easy ML Kit"; } }

        public static string Version { get { return "2.0.0"; } }

        public static string DefaultSettingsAssetName { get { return "EasyMLKitSettings"; } }

        public static string DefaultSettingsAssetPath { get { return "Assets/Samples/Resources/" + DefaultSettingsAssetName + ".asset"; } }

        public static EasyMLKitSettings Instance
        {
            get { return GetSharedInstanceInternal(); }
        }

        public static bool IsConfigured()
        {
            return IOServices.FileExists(DefaultSettingsAssetPath);
        }

        #endregion

        #region Properties

        public BarcodeScannerUnitySettings BarcodeScannerSettings
        {
            get
            {
                return m_barcodeScannerSettings;
            }
            set
            {
                m_barcodeScannerSettings = value;
            }
        }

        public ObjectDetectorAndTrackerUnitySettings ObjectDetectorAndTrackerSettings {
            get
            {
                return m_objectDetectorAndTrackerSettings;
            }
            set
            {
                m_objectDetectorAndTrackerSettings = value;
            }
        }

        public TextRecognizerUnitySettings TextRecognizerSettings
        {
            get
            {
                return m_textRecognizerSettings;
            }
            set
            {
                m_textRecognizerSettings = value;
            }
        }

        public DigitalInkRecognizerUnitySettings DigitalInkRecognizerSettings
        {
            get
            {
                return m_digitalInkRecognizerSettings;
            }
            set
            {
                m_digitalInkRecognizerSettings = value;
            }
        }

        public FaceDetectorUnitySettings FaceDetectorSettings
        {
            get
            {
                return m_faceDetectorSettings;
            }
            set
            {
                m_faceDetectorSettings = value;
            }
        }

        #endregion

        #region Static methods

        public static void SetSettings(EasyMLKitSettings settings)
        {
            Assert.IsArgNotNull(settings, nameof(settings));

            // set properties
            s_sharedInstance    = settings;
        }

        private static EasyMLKitSettings GetSharedInstanceInternal(bool throwError = true)
        {
            if (null == s_sharedInstance)
            {
                // check whether we are accessing in edit or play mode
                var     assetPath   = DefaultSettingsAssetName;
                var     settings    = Resources.Load<EasyMLKitSettings>(assetPath);
                if (throwError && (null == settings))
                {
                    throw Diagnostics.PluginNotConfiguredException();
                }

                // store reference
                s_sharedInstance = settings;
            }

            return s_sharedInstance;
        }

        #endregion

        #region Feature methods

        public string[] GetAvailableFeatureNames()
        {
            return new string[]
            {
                NativeFeatureType.kBarcodeScanner,
                NativeFeatureType.kObjectDetectorAndTracker,
                NativeFeatureType.kTextRecognizer
            };
        }

        public string[] GetUsedFeatureNames()
        {
            var     usedFeatures    = new List<string>();
            if (m_barcodeScannerSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kBarcodeScanner);
            }

            if (m_objectDetectorAndTrackerSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kObjectDetectorAndTracker);
            }

            if (m_textRecognizerSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kTextRecognizer);
            }

            if (m_faceDetectorSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kFaceDetector);
            }

            if (m_digitalInkRecognizerSettings.IsEnabled)
            {
                usedFeatures.Add(NativeFeatureType.kDigitalInkRecognizer);
            }

            return usedFeatures.ToArray();
        }

        public bool IsFeatureUsed(string name)
        {
            return Array.Exists(GetUsedFeatureNames(), (item) => string.Equals(item, name));
        }

        #endregion

        private void OnValidate()
        {
#if UNITY_EDITOR
            UnityEditor.EditorPrefs.SetBool("refresh-feature-dependencies", true);
#endif
        }
    }
}