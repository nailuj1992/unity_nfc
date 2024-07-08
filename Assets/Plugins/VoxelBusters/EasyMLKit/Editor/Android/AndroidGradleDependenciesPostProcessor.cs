#if UNITY_EDITOR && UNITY_ANDROID
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor.Android;
using System;
using UnityEngine;
using System.Collections.Generic;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.CoreLibrary;
using System.Linq;

namespace VoxelBusters.EasyMLKit.Editor.Android
{
    public class AndroidGradleDependenciesPostProcessor : IPostGenerateGradleAndroidProject
    {
        Dictionary<string, string> m_libraryMap = new Dictionary<string, string>()
        {
            { NativeFeatureType.kBarcodeScanner, "com.voxelbusters.easymlkit.barcode-scanner.aar" },
            { NativeFeatureType.kObjectDetectorAndTracker, "com.voxelbusters.easymlkit.object-detection-and-tracking.aar" },
            { NativeFeatureType.kTextRecognizer, "com.voxelbusters.easymlkit.text-recognition-v2.aar" },
            { NativeFeatureType.kDigitalInkRecognizer, "com.voxelbusters.easymlkit.digital-ink-recognition.aar" }
        };

        public void OnPostGenerateGradleAndroidProject(string basePath)
        {
            if (!EasyMLKitSettings.IsConfigured())
            {
                return;
            }

            List<string> deletedLibraries = new List<string>();

            foreach(string eachFeature in m_libraryMap.Keys)
            {
                if(!EasyMLKitSettings.Instance.IsFeatureUsed(eachFeature))
                {
                    try
                    {
                        string libraryName = m_libraryMap[eachFeature];
                        IOServices.DeleteFile(Path.Combine(basePath, "libs", libraryName), true);
                        deletedLibraries.Add(libraryName.Replace(".aar", ""));
                    }
                    catch(Exception e)
                    {
                        DebugLogger.LogError("Failed finding the library file to delete : " + e);
                    }
                }
            }

            //Update build.gradle file
            UpdateGradleFile(basePath, deletedLibraries);
        }

        public int callbackOrder { get { return 1; } }

        private void UpdateGradleFile(string basePath, List<string> deletedLibraries)
        {
            string gradlePath = IOServices.CombinePath(basePath, "build.gradle");
            string[] lines = File.ReadAllLines(gradlePath);
            StringBuilder builder = new StringBuilder();

            foreach (string each in lines)
            {
                if(!deletedLibraries.Any(eachLib => each.Contains(eachLib)))
                {
                    builder.AppendLine(each);
                }
            }
            File.WriteAllText(gradlePath, builder.ToString());
        }
    }
}
#endif
