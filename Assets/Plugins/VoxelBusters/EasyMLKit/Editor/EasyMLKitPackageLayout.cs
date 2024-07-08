#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary.Editor;

namespace VoxelBusters.EasyMLKit.Editor
{
    internal static class EasyMLKitPackageLayout
    {
        public static string ExtrasPath { get { return "Assets/Plugins/VoxelBusters/EasyMLKit"; } }

        public static string EditorExtrasPath { get { return ExtrasPath + "/Editor"; } }

        public static string IosPluginPath { get { return ExtrasPath + "/Plugins/iOS"; } }

        public static string AndroidPluginPath { get { return "Assets/Plugins/Android"; } }

        public static string EditorResourcesPath { get { return ExtrasPath + "/EditorResources"; } }

        // android
        public static string AndroidEditorSourcePath { get { return ExtrasPath + "/Editor" + "/Android"; } }
        public static string AndroidProjectFolderName { get { return "com.voxelbusters.easymlkit.androidlib"; } }
        public static string AndroidProjectPath { get { return AndroidPluginPath + "/" + AndroidProjectFolderName; } }
    }
}
#endif