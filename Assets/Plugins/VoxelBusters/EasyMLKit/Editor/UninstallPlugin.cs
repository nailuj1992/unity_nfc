#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.EasyMLKit.Editor
{
	public class UninstallPlugin
	{
		#region Constants

		private const	string		kTitle = "Easy ML Kit 1.0.0";

		private const	string		kUninstallAlertTitle		= "Uninstall - " + kTitle;
		private const	string		kUninstallCommonAlertTitle	= "Uninstall Common Folders";

		private const	string		kUninstallAlertMessage		= "Backup before doing this step to preserve changes done in this plugin. This deletes files only related to Voxel Busters plugins. Do you want to proceed?";

		private	static	readonly	string[]	kPluginFolders	= new string[]
		{
			EasyMLKitPackageLayout.ExtrasPath
        };

		private static readonly string[] kCommonFolders = new string[]
		{
					EasyMLKitPackageLayout.ExtrasPath + "/../" + "com.voxelbusters.corelibrary",
					EasyMLKitPackageLayout.ExtrasPath + "/../" + "com.voxelbusters.internal",
					EasyMLKitPackageLayout.ExtrasPath + "/../" + "com.voxelbusters.parser",
		};

		#endregion

		#region Static methods
		[MenuItem("Window/Voxel Busters/Easy ML Kit/Uninstall")]
		public static void Uninstall()
		{
			bool	confirmationStatus		= EditorUtility.DisplayDialog(kUninstallAlertTitle, kUninstallAlertMessage, "Uninstall", "Cancel");
			if (confirmationStatus)
			{
				Delete(kPluginFolders);

				if(EditorUtility.DisplayDialog(kUninstallCommonAlertTitle, kUninstallAlertMessage, "Uninstall", "Cancel"))
                {
					Delete(kCommonFolders);
				}
				
				AssetDatabase.Refresh();
				EditorUtility.DisplayDialog(kTitle,
				                            "Uninstall successful!", 
				                            "Ok");
			}
		}

		private static void Delete(string[] folders)
        {
			foreach (string folder in folders)
			{
				string absolutePath = Application.dataPath + "/../" + folder;
				FileUtil.DeleteFileOrDirectory(absolutePath);
				FileUtil.DeleteFileOrDirectory(absolutePath + ".meta");
			}
		}

		#endregion
	}
}
#endif