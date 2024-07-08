#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#else
using UnityEngine.Experimental.UIElements;
#endif

namespace VoxelBusters.EasyMLKit.Editor
{
    public class EasyMLKitSettingsProvider : SettingsProvider
    {
        #region Fields

        private     EasyMLKitSettingsInspector      m_settingsInspector;

        #endregion

        #region Constructors

        public EasyMLKitSettingsProvider(string path, SettingsScope scopes)
            : base(path, scopes)
        {
            // set properties
            keywords    = GetSearchKeywordsFromSerializedObject(new SerializedObject(EasyMLKitSettingsEditorUtility.DefaultSettings));
        }

        #endregion

        #region Static methods

        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            return new EasyMLKitSettingsProvider(path: "Project/Voxel Busters/Easy ML Kit", scopes: SettingsScope.Project);
        }

        #endregion

        #region Base class methods

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            base.OnActivate(searchContext, rootElement);

            m_settingsInspector = UnityEditor.Editor.CreateEditor(EasyMLKitSettingsEditorUtility.DefaultSettings) as EasyMLKitSettingsInspector;
        }

        public override void OnTitleBarGUI()
        {
            var     settings    = EasyMLKitSettingsEditorUtility.DefaultSettings;
            EditorGUILayout.InspectorTitlebar(false, settings);
        }

        public override void OnGUI(string searchContext)
        {
            if (m_settingsInspector == null)
            {
                return;
            }
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10f);
            EditorGUILayout.BeginVertical();
            m_settingsInspector.OnInspectorGUI();
            EditorGUILayout.EndVertical();
            GUILayout.Space(10f);
            EditorGUILayout.EndHorizontal();
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();

            if (m_settingsInspector)
            {
                Object.DestroyImmediate(m_settingsInspector);
                m_settingsInspector = null;
            }
        }

        #endregion
    }
}
#endif