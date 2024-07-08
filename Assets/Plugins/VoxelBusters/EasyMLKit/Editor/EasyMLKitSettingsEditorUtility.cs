#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VoxelBusters.CoreLibrary.Editor;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Editor
{
    public static class EasyMLKitSettingsEditorUtility
    {
        #region Static fields

        private     static      EasyMLKitSettings        s_defaultSettings       = null;

        #endregion

        #region Static properties

        public static EasyMLKitSettings DefaultSettings
        {
            get
            {
                if (s_defaultSettings == null)
                {
                    s_defaultSettings = LoadDefaultSettings(throwError: false);

                    if(s_defaultSettings == null)
                        s_defaultSettings = CreateDefaultSettings();
                }
                return s_defaultSettings;
            }
            set
            {
                Assert.IsPropertyNotNull(value, nameof(value));

                // set new value
                s_defaultSettings       = value;
            }
        }

        public static bool SettingsExists
        {
            get
            {
                if (s_defaultSettings == null)
                {
                    s_defaultSettings   = LoadDefaultSettings();
                }
                return (s_defaultSettings != null);
            }
        }

        #endregion

        #region Static methods

        public static void ShowSettingsNotFoundErrorDialog()
        {
            EditorUtility.DisplayDialog(
                title: "Error",
                message: "Easy ML Kit plugin is not configured. Please select plugin settings file from menu and configure it according to your preference.",
                ok: "Ok");
        }

        #endregion

        #region Private static methods

        private static EasyMLKitSettings CreateDefaultSettings()
        {
            string  filePath    = EasyMLKitSettings.DefaultSettingsAssetPath;
            var     settings    = ScriptableObject.CreateInstance<EasyMLKitSettings>();
            SetDefaultProperties(settings);

            // create file
            AssetDatabaseUtility.CreateAssetAtPath(settings, filePath);
            AssetDatabase.Refresh();

            return settings;
        }

        private static EasyMLKitSettings LoadDefaultSettings(bool throwError = true)
        {
            string  filePath    = EasyMLKitSettings.DefaultSettingsAssetPath;
            var     settings    = AssetDatabase.LoadAssetAtPath<EasyMLKitSettings>(filePath);
            if (settings)
            {
                SetDefaultProperties(settings);
                return settings;
            }

            if (throwError)
            {
                throw Diagnostics.PluginNotConfiguredException();
            }

            return null;
        }

        private static void SetDefaultProperties(EasyMLKitSettings settings)
        {
            
        }

        #endregion
    }
}
#endif