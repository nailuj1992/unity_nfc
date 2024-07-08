using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.CoreLibrary.Editor.NativePlugins.Build
{
    public abstract class NativePluginsExporter : IPostprocessBuildWithReport
    {
        #region Constants

        private     const   string      kBaseExporterName       = "Base";

        #endregion

        #region Proprerties

        public NativePluginsExporterSettings[] ActiveExporters { get; private set; }

        public BuildReport Report { get; private set; }

        public string OutputPath => Report.summary.outputPath;

        #endregion

        #region Static methods

        private static NativePluginsExporterSettings[] FindActiveExporterSettings()
        {
            var     exporters           = NativePluginsExporterSettings.FindAllExporters(includeInactive: true);
            var     baseExporter        = Array.Find(exporters, (item) => string.Equals(item.name, kBaseExporterName));
            if (baseExporter != null)
            {
                baseExporter.IsEnabled  = true; // Array.Exists(exporters, (item) => (item != baseExporter) && item.IsEnabled);
            }
            var     canToggleFeatures   = SettingsPropertyGroup.CanToggleFeatureUsageState();
            return Array.FindAll(exporters, (item) => item.IsEnabled || !canToggleFeatures);
        }

        #endregion

        #region Base class methods

        protected virtual void Init()
        { }

        protected virtual bool CanPerformExport(BuildTarget target) => true;

        protected virtual void PerformExport()
        { }

        #endregion

        #region Private methods

        private void InitInternal(BuildReport report)
        {
            // Set properties
            Report          = report;
            ActiveExporters = FindActiveExporterSettings();

            // Invoke concrete implementation
            Init();
        }

        #endregion

        #region IPostprocessBuildWithReport implementation

        int IOrderedCallback.callbackOrder => int.MaxValue;

        void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport report)
        {
            if (!CanPerformExport(target: report.summary.platform)) return;

            InitInternal(report);
            PerformExport();
        }

        #endregion
    }
}