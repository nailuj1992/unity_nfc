#if UNITY_EDITOR
using UnityEditor;

namespace VoxelBusters.EasyMLKit.Editor
{
    public static class EasyMLKitMenuManager
    {
        #region Constants

        private const string kMenuItemPath = "Window/Voxel Busters/Easy ML Kit";

        #endregion

        #region Menu items

        [MenuItem(kMenuItemPath + "/Open Settings")]
        public static void OpenSettings()
        {
            Selection.activeObject  = null;
            SettingsService.OpenProjectSettings("Project/Voxel Busters/Easy ML Kit");
        }


        [MenuItem(kMenuItemPath + "/Uninstall")]
        public static void Uninstall()
        {
            UninstallPlugin.Uninstall();
        }

        #endregion
    }
}
#endif