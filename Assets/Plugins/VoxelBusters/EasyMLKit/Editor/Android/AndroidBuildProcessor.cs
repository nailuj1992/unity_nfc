#if UNITY_EDITOR && UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Editor.Build
{
    public class AndroidBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            // check whether plugin is configured
            if (!EasyMLKitSettingsEditorUtility.SettingsExists)
            {
                return;
            }

            DebugLogger.Log("[Easy ML Kit] Initiating pre-build task execution.");

            EasyMLKitBuildUtility.CreateStrippingFile(report.summary.platform);

            DebugLogger.Log("[Easy ML Kit] Successfully completed pre-build task execution.");
        }
    }
}
#endif
