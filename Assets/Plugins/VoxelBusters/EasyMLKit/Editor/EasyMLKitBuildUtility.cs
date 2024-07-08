#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.Editor;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.CoreLibrary.Editor.NativePlugins.Build;

namespace VoxelBusters.EasyMLKit.Editor
{
    public static class EasyMLKitBuildUtility 
    {
        #region Stripping files

        public static void CreateStrippingFile(BuildTarget buildTarget)
        {
            // check whether plugin is configured
            if (!EasyMLKitSettingsEditorUtility.SettingsExists)
            {
                EasyMLKitSettingsEditorUtility.ShowSettingsNotFoundErrorDialog();
                return;
            }

            // generate stripping file
            var     settings            = EasyMLKitSettingsEditorUtility.DefaultSettings;
            var     strippingWriter     = new LinkerStrippingSettingsWriter(path: $"{EasyMLKitPackageLayout.ExtrasPath}/link.xml");
            var     availableFeatures   = settings.GetAvailableFeatureNames();
            var     usedFeatures        = settings.GetUsedFeatureNames();
            if (IsReleaseBuild() && usedFeatures.Length > 0)
            {
                var     platform        = EditorApplicationUtility.ConvertBuildTargetToRuntimePlatform(buildTarget);
                foreach (string feature in availableFeatures)
                {
                    var     featureConfiguration    = ImplementationBlueprint.GetRuntimeConfiguration(feature);
                    if (!Array.Exists(usedFeatures, (item) => string.Equals(feature, item)))
                    {
                        continue;
                    }

                    var     packageConfiguration    = featureConfiguration.GetPackageForPlatform(platform);
                    if (packageConfiguration == null)
                    {
                        DebugLogger.LogWarning("Configuration not found for feature: " +  feature);
                        var     fallbackConfiguration   = featureConfiguration.FallbackPackage;
                        strippingWriter.AddRequiredType(fallbackConfiguration.Assembly, fallbackConfiguration.NativeInterfaceType);
                    }
                    else
                    {
                        strippingWriter.AddRequiredNamespace(packageConfiguration.Assembly, packageConfiguration.Namespace);
                        strippingWriter.AddRequiredNamespace(packageConfiguration.Assembly, packageConfiguration.Namespace + ".Internal");
                    }
                }
            }
            strippingWriter.WriteToFile();
        }

        public static bool IsReleaseBuild()
        {
            var     firstPackage    = ImplementationBlueprint.BarcodeScanner.GetPackageForPlatform(RuntimePlatform.OSXEditor);
            return !(firstPackage == null || ReflectionUtility.FindAssemblyWithName(firstPackage.Assembly) == null);
        }

        #endregion
    }
}
#endif