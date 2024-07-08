#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Editor.Build
{
    public class UnsupportedPlatformBuildProcessor : IPreprocessBuildWithReport
    {
        #region Static methods

        private static bool IsBuildTargetSupported(BuildTarget buildTarget)
        {
            return ((BuildTarget.iOS == buildTarget) || (BuildTarget.Android == buildTarget));
        }

        #endregion

        #region IPreprocessBuildWithReport implementation

        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            // check whether plugin is configured
            if (!EasyMLKitSettingsEditorUtility.SettingsExists || IsBuildTargetSupported(report.summary.platform))
            {
                return;
            }

            DebugLogger.Log("[Easy ML Kit] Initiating pre-build task execution.");

            EasyMLKitBuildUtility.CreateStrippingFile(report.summary.platform);

            DebugLogger.Log("[Easy ML Kit] Successfully completed pre-build task execution.");
        }

        #endregion
    }
}
#endif