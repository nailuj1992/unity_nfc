#if UNITY_IOS || UNITY_TVOS
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace VoxelBusters.CoreLibrary.Editor.NativePlugins.Build.Xcode
{
    public class PBXNativePluginsExporter : NativePluginsExporter, IPostprocessBuildWithReport
    {
        #region Constants

        private const string                kPluginRelativePath     = "VoxelBusters/";

        private static readonly string[]    kIgnoreFileExtensions   = new string[]
        {
            ".meta", 
            ".pdf",
            ".DS_Store",
            ".mdown",
            ".asset",
            ".cs",
        };

        #endregion

        #region Fields

        private     PBXProject                  m_project;
        private     ProjectCapabilityManager    m_capabilityManager;
        private     List<string>                m_librarySearchPaths;
        private     List<string>                m_frameworkSearchPaths;
        
        #endregion

        #region Properties

        protected PBXProject Project
        {
            get
            {
                if (m_project == null)
                {
                    m_project   = new PBXProject();
                    m_project.ReadFromFile(ProjectFilePath);
                }
                return m_project;
            }
        }

        protected string ProjectFilePath => PBXProject.GetPBXProjectPath(OutputPath);

        protected ProjectCapabilityManager CapabilityManager
        {
            get
            {
                if (m_capabilityManager == null)
                {
                    m_capabilityManager         = new ProjectCapabilityManager(
                        ProjectFilePath,
                        GetEntitlementsPath(),
                        Project.GetMainTargetName(),
                        Project.GetMainTargetGuid());
                }
                return m_capabilityManager;
            }
        }

        #endregion

        #region Base class methods

        protected override void Init()
        {
            base.Init();

            // Set properties
            m_librarySearchPaths        = new List<string>();
            m_frameworkSearchPaths      = new List<string>();
            
            // Remove old directory
            string  pluginExportPath    = Path.Combine(OutputPath, kPluginRelativePath);
            IOServices.DeleteDirectory(pluginExportPath);
        }

        #endregion

        #region Static methods

        protected override bool CanPerformExport(BuildTarget target)
        {
            return (BuildTarget.iOS == target) || (BuildTarget.tvOS == target);
        }

        protected override void PerformExport()
        {
            base.PerformExport();

            UpdateMacroDefinitions();
            UpdatePBXProject();
        }

        #endregion

        #region Private methods

        private void UpdatePBXProject()
        {
            DebugLogger.Log(CoreLibraryDomain.Default, "Linking native files.");

            // Open project file for editing
            string  projectFilePath     = ProjectFilePath;
            var     project             = Project;
            string  mainTargetGuid      = project.GetMainTargetGuid();
            string  frameworkTargetGuid = project.GetFrameworkGuid();
            DebugLogger.Log(CoreLibraryDomain.NativePlugins, $"Project File Path: {projectFilePath} targetGuid: {frameworkTargetGuid} ProjectPath: {OutputPath}");

            project.AddSourcesBuildPhase(frameworkTargetGuid);//@@ fix for "does not refer to a file in a known build section"

            // Read exporter settings for adding native files 
            foreach (var featureExporter in ActiveExporters)
            {
                DebugLogger.Log(CoreLibraryDomain.Default, $"Is feature: {featureExporter.name} enabled:{featureExporter.IsEnabled}.");
                string  exporterFilePath    = Path.GetFullPath(AssetDatabase.GetAssetPath(featureExporter));
                string  exporterFolder      = Path.GetDirectoryName(exporterFilePath);
                var     iosSettings         = featureExporter.IosProperties;
                string  exporterGroup       = GetExportGroupPath(exporterSettings: featureExporter, prefixPath: kPluginRelativePath);

                // Add files
                foreach (var fileInfo in iosSettings.Files)
                {
                    AddFileToProject(project, fileInfo.AbsoultePath, frameworkTargetGuid, exporterGroup, fileInfo.CompileFlags);
                }

                // Add folder
                foreach (var folderInfo in iosSettings.Folders)
                {
                    AddFolderToProject(project, folderInfo.AbsoultePath, frameworkTargetGuid, exporterGroup, folderInfo.CompileFlags);
                }

                // Add headerpaths
                foreach (var pathInfo in iosSettings.HeaderPaths)
                {
                    string  destinationPath = GetFilePathInProject(pathInfo.AbsoultePath, exporterFolder, exporterGroup);
                    string  formattedPath   = FormatFilePathInProject(destinationPath);
                    project.AddHeaderSearchPath(frameworkTargetGuid, formattedPath);
                }

                // Add frameworks
                foreach (var framework in iosSettings.Frameworks)
                {
                    if (framework.Target.HasFlag(PBXTargetMembership.UnityIphone))
                    {
                        project.AddFrameworkToProject(mainTargetGuid, framework.Name, framework.IsOptional);
                    }
                    if (framework.Target.HasFlag(PBXTargetMembership.UnityFramework))
                    {
                        project.AddFrameworkToProject(frameworkTargetGuid, framework.Name, framework.IsOptional);
                    }
                }

                // Add build properties
                foreach (var property in iosSettings.BuildProperties)
                {
                    project.AddBuildProperty(frameworkTargetGuid, property.Key, property.Value);
                }
            }

            // Add header search paths
            foreach (string path in m_librarySearchPaths)
            {
                project.AddLibrarySearchPath(frameworkTargetGuid, FormatFilePathInProject(path));
            }

            // Add framework search paths
            foreach (string path in m_frameworkSearchPaths)
            {
                project.AddFrameworkSearchPath(frameworkTargetGuid, FormatFilePathInProject(path));
            }

            // Add additional files
            CopyRequiredStreamingAssetsToProjectRoot();
            UpdateCapabilities();

            // Apply changes
            File.WriteAllText(projectFilePath, project.WriteToString());
        }

        private string GetExportGroupPath(NativePluginsExporterSettings exporterSettings, string prefixPath)
        {
            string  groupPath               = prefixPath;
            bool    usesNestedHierarchy     = true;
            if (exporterSettings.Group != null)
            {
                groupPath                  += exporterSettings.Group.Name + "/";
                usesNestedHierarchy         = exporterSettings.Group.UsesNestedHeierarchy;
            }
            if (usesNestedHierarchy)
            {
                groupPath                  += exporterSettings.name + "/";
            }
            return groupPath;
        }

        private string GetEntitlementsPath()
		{
			var     targetGuid      = Project.GetMainTargetGuid();
			var     targetName      = Project.GetMainTargetName();

            var     relativePath    = Project.GetBuildPropertyForAnyConfig(targetGuid, PBXBuildConfigurationKey.kCodeSignEntitlements);
			if (relativePath != null)
			{
				var     fullPath    = Path.Combine(OutputPath, relativePath);
				if (IOServices.FileExists(fullPath))
				{
					return fullPath;
				}
			}

            //  Make new file
			var     entitlementsPath    = Path.Combine(OutputPath, targetName, $"{targetName}.entitlements");
			var     entitlementsPlist   = new PlistDocument();
			entitlementsPlist.WriteToFile(entitlementsPath);

			// Copy the entitlement file to the xcode project
			var     entitlementFileName = Path.GetFileName(entitlementsPath);
			var     relativeDestination = $"{targetName}/{entitlementFileName}";

			// Add the pbx configs to include the entitlements files on the project
			Project.AddFile(relativeDestination, entitlementFileName);
			Project.SetBuildProperty(targetGuid, PBXBuildConfigurationKey.kCodeSignEntitlements, relativeDestination);

			return entitlementsPath;
		}

        private void UpdateCapabilities()
        {
            foreach (var featureExporter in ActiveExporters)
            {
                if (!featureExporter.IsEnabled) continue;
                    
                foreach (var capability in featureExporter.IosProperties.Capabilities)
                {
                    switch (capability.Type)
                    {
                        case PBXCapabilityType.GameCenter:
                            CapabilityManager.AddGameCenter();
                            break;

                        case PBXCapabilityType.iCloud:
                            CapabilityManager.AddiCloud(enableKeyValueStorage: true, enableiCloudDocument: false, enablecloudKit: false, addDefaultContainers: false, customContainers: null);
                            break;

                        case PBXCapabilityType.InAppPurchase:
                            CapabilityManager.AddInAppPurchase();
                            break;

                        case PBXCapabilityType.PushNotifications:
                            CapabilityManager.AddPushNotifications(Debug.isDebugBuild);
                            CapabilityManager.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);
                            break;

                        case PBXCapabilityType.AssociatedDomains:
                            var     associatedDomainsEntitlement    = capability.AssociatedDomainsEntitlement;
                            CapabilityManager.AddAssociatedDomains(domains: associatedDomainsEntitlement.Domains);
                            break;

                        default:
                            throw VBException.SwitchCaseNotImplemented(capability.Type);
                    }
                }
            }

            // save changes
            CapabilityManager.WriteToFile();
        }

        private void UpdateMacroDefinitions()
        {
            var     requiredMacros  = new List<string>();
            foreach (var featureExporter in ActiveExporters)
            {
                var     macros      = featureExporter.IosProperties.Macros;
                foreach (var entry in macros)
                {
                    requiredMacros.AddUnique(entry);
                }
            }

            PreprocessorDirectives.WriteMacros(requiredMacros.ToArray());
        }

        private void AddFileToProject(PBXProject project, string sourceFilePath, string targetGuid, string parentGroup, string[] compileFlags)
        {
            // convert relative path to absolute path
            if (!Path.IsPathRooted(sourceFilePath))
            {
                sourceFilePath          = Path.GetFullPath(sourceFilePath);
            }

            // copy file to the target folder
            string  fileName            = Path.GetFileName(sourceFilePath);
            string  destinationFolder   = IOServices.CombinePath(OutputPath, parentGroup);
            string  destinationFilePath = CopyFileToProject(sourceFilePath, destinationFolder);
            DebugLogger.Log(CoreLibraryDomain.Default, $"Adding file {fileName} to project.");

            // add copied file to the project
            string  fileGuid            = project.AddFile(FormatFilePathInProject(destinationFilePath, rooted: false),  parentGroup + fileName);
            project.AddFileToBuildWithFlags(targetGuid, fileGuid, string.Join(" ", compileFlags));

            // add search path project
            string  fileExtension       = Path.GetExtension(destinationFilePath);
            if (string.Equals(fileExtension, ".a", StringComparison.InvariantCultureIgnoreCase))
            {
                CacheLibrarySearchPath(destinationFilePath);
            }
            else if (string.Equals(fileExtension, ".framework", StringComparison.InvariantCultureIgnoreCase))
            {
                CacheFrameworkSearchPath(destinationFilePath);
            }
        }

        private void AddFolderToProject(PBXProject project, string sourceFolder, string targetGuid, string parentGroup, string[] compileFlags)
        {
            // check whether given folder is valid
            var     sourceFolderInfo    = new DirectoryInfo(sourceFolder);
            if (!sourceFolderInfo.Exists) return;

            // add files placed within this folder
            foreach (var fileInfo in FindFiles(sourceFolderInfo))
            {
                AddFileToProject(project, fileInfo.FullName, targetGuid, parentGroup, compileFlags);
            }

            // add folders placed within this folder
            foreach (var subFolderInfo in sourceFolderInfo.GetDirectories())
            {
                string  folderGroup     = parentGroup + subFolderInfo.Name + "/";
                AddFolderToProject(project, subFolderInfo.FullName, targetGuid, folderGroup, compileFlags);
            }
        }

        private string CopyFileToProject(string filePath, string targetFolder)
        {
#if NATIVE_PLUGINS_DEBUG
            return filePath;
#else
            // create target folder directory, incase if it doesn't exist
            if (!IOServices.DirectoryExists(targetFolder))
            {
                IOServices.CreateDirectory(targetFolder);
            }

            // copy specified file
            string  fileName        = Path.GetFileName(filePath);
            string  destPath        = Path.Combine(targetFolder, fileName);

            DebugLogger.Log(CoreLibraryDomain.NativePlugins, $"Copying file {filePath} to {destPath}.");
            FileUtil.CopyFileOrDirectory(filePath, destPath);

            return destPath;
#endif
        }

        private string GetFilePathInProject(string sourcePath, string parentFolder, string parentGroup)
        {
#if NATIVE_PLUGINS_DEBUG
            return sourcePath;
#else
            string  relativePath        = IOServices.GetRelativePath(parentFolder, sourcePath);
            string  destinationFolder   = IOServices.CombinePath(OutputPath, parentGroup);
            return IOServices.CombinePath(destinationFolder, relativePath);
#endif
        }

        private string FormatFilePathInProject(string path, bool rooted = true)
        {
#if NATIVE_PLUGINS_DEBUG
            return path;
#else
            if (path.Contains("$(inherited)"))
            {
                return path;
            }

            string  relativePathToProject   = IOServices.GetRelativePath(OutputPath, path);
            return rooted ? Path.Combine("$(SRCROOT)", relativePathToProject) : relativePathToProject;
#endif
        }

        private void CacheLibrarySearchPath(string path)
        {
            string  directoryPath   = Path.GetDirectoryName(path);
            m_librarySearchPaths.AddUnique(directoryPath);
        }

        private void CacheFrameworkSearchPath(string path)
        {
            string  directoryPath   = Path.GetDirectoryName(path);
            m_frameworkSearchPaths.AddUnique(directoryPath);
        }

        private FileInfo[] FindFiles(DirectoryInfo folder)
        {
            return folder.GetFiles().Where((fileInfo) =>
            {
                string  fileExtension   = fileInfo.Extension;
                return !Array.Exists(kIgnoreFileExtensions, (ignoreExt) => string.Equals(fileExtension, ignoreExt, StringComparison.InvariantCultureIgnoreCase));
            }).ToArray();
        }

        //Added for supporting notification services custom sound files
        private void CopyRequiredStreamingAssetsToProjectRoot()
        {
            string  mainTargetGuid  = Project.GetMainTargetGuid();

            // Copy audio files from streaming assets if any to Raw folder
            string  path            = UnityEngine.Application.streamingAssetsPath;
            if (IOServices.DirectoryExists(path))
            {
                var     files               = System.IO.Directory.GetFiles(path);
                string  destinationFolder   = OutputPath;

                var     formats             = new string[]
                {
                    ".mp3",
                    ".wav",
                    ".ogg",
                    ".aiff"
                };
                for (int i=0; i< files.Length; i++)
                {
                    string  extension   = IOServices.GetExtension(files[i]);
                    if (formats.Contains(extension.ToLower()))
                    {
                        string destinationRelativePath = files[i].Replace(path, ".");
                        IOServices.CopyFile(files[i], IOServices.CombinePath(destinationFolder, IOServices.GetFileName(files[i])));
                        DebugLogger.Log(CoreLibraryDomain.NativePlugins, $"Coping asset with relativePath: {destinationRelativePath}.");
                        Project.AddFileToBuild(mainTargetGuid, Project.AddFile(destinationRelativePath, destinationRelativePath));
                    }
                }
            }
        }

        #endregion
    }
}
#endif