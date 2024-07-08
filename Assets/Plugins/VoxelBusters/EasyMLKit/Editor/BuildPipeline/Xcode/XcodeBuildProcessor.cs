#if UNITY_EDITOR && (UNITY_IOS || UNITY_TVOS)
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.Editor.NativePlugins;
using VoxelBusters.CoreLibrary.Editor.NativePlugins.Build.Xcode;
using UnityEditor.Build.Reporting;
using UnityEditor.Build;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.CoreLibrary.Editor.NativePlugins.Build;

namespace VoxelBusters.EasyMLKit.Editor.Build.Xcode
{
    public class XcodeBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
#region Static fields

        private     static  EasyMLKitSettings    s_settings;

        private     static  BuildReport             s_buildReport;

#endregion

#region IPreprocessBuildWithReport implementation

        public int callbackOrder => 99;

        public void OnPreprocessBuild(BuildReport report)
        {
            // check whether plugin is configured
            if (!EasyMLKitSettingsEditorUtility.SettingsExists)
            {
                EasyMLKitSettingsEditorUtility.ShowSettingsNotFoundErrorDialog();
                return;
            }
            // check whether target is compatible for these tasks
            if (!IsBuildTargetSupported(report.summary.platform))
            {
                return;
            }

            DebugLogger.Log("[XcodeBuildProcess] Initiating pre-build task execution.");
            
            // update cached information
            s_settings      = EasyMLKitSettingsEditorUtility.DefaultSettings;
            s_buildReport   = report;

            // execute tasks
            UpdateExporterSettings();
            EasyMLKitBuildUtility.CreateStrippingFile(report.summary.platform);

            DebugLogger.Log("[XcodeBuildProcess] Successfully completed pre-build task execution.");
        }

#endregion

#region IPostprocessBuildWithReport implementation

        public void OnPostprocessBuild(BuildReport report)
        {
            // check whether plugin is configured
            if (!EasyMLKitSettingsEditorUtility.SettingsExists)
            {
                EasyMLKitSettingsEditorUtility.ShowSettingsNotFoundErrorDialog();
                return;
            }
            if (!IsBuildTargetSupported(report.summary.platform))
            {
                return;
            }

            DebugLogger.Log("[XcodeBuildProcess] Initiating post-build task execution.");

            // update cached information
            s_settings      = EasyMLKitSettingsEditorUtility.DefaultSettings;
            s_buildReport   = report;

            // execute tasks
            UpdateInfoPlist();

            DebugLogger.Log("[XcodeBuildProcess] Successfully completed post-build task execution.");
        }

#endregion

#region Private methods

        private static bool IsBuildTargetSupported(BuildTarget buildTarget)
        {
            return (BuildTarget.iOS == buildTarget || BuildTarget.tvOS == buildTarget);
        }

        private static void UpdateExporterSettings()
        {
            DebugLogger.Log("[XcodeBuildProcess] Updating native plugins exporter settings.");

            bool    enableBaseExporter  = false;
            foreach (var exporter in NativePluginsExporterSettings.FindAllExporters(includeInactive: true))
            {
                switch (exporter.name)
                {
                    case NativeFeatureType.kBarcodeScanner:
                        exporter.IsEnabled  = s_settings.IsFeatureUsed(exporter.name); 
                        enableBaseExporter |= exporter.IsEnabled;     
                        break;

                    case NativeFeatureType.kTextRecognizer:
                        exporter.IsEnabled = s_settings.IsFeatureUsed(exporter.name);
                        enableBaseExporter |= exporter.IsEnabled;
                        break;
                    default:
                        break;
                }
                EditorUtility.SetDirty(exporter);
            }
        }

        private static void UpdateInfoPlist()
        {
            DebugLogger.Log("[XcodeBuildProcess] Updating plist configuration.");

            // open plist
            string  plistPath   = s_buildReport.summary.outputPath + "/Info.plist";
            var     plist       = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            var     rootDict    = plist.root;

            // add usage permissions
            var     permissions = GetUsagePermissions();
            foreach (string key in permissions.Keys)
            {
                rootDict.SetString(key, permissions[key]);
            }

            // save changes to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }

        private static Dictionary<string, string> GetUsagePermissions()
        {
            var     requiredPermissionsDict     = new Dictionary<string, string>();

            requiredPermissionsDict[InfoPlistKey.kNSCameraUsage] = "Uses camera for scanning";
            return requiredPermissionsDict;
        }

#endregion
    }
}
#endif