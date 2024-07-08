#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.CoreLibrary.Editor;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Editor
{
    [CustomEditor(typeof(EasyMLKitSettings))]
    public class EasyMLKitSettingsInspector : UnityEditor.Editor
    {
        #region Fields

        // internal properties
        private     PropertyGroupMeta[]     m_propertyMetaArray             = null;

        private     SerializedProperty[]    m_properties                    = null;

        private     int                     m_propertyCount                 = 0;

        private     SerializedProperty      m_activeProperty                = null;

        private     string                  m_formattedVersion;

        // custom gui styles
        private     GUIStyle                m_groupBackgroundStyle          = null;

        private     GUIStyle                m_headerStyle                   = null;

        private     GUIStyle                m_headerFoldoutStyle            = null;

        private     GUIStyle                m_headerLabelStyle              = null;

        private     GUIStyle                m_headerToggleStyle             = null;

        // assets
        private     Texture2D               m_logoIcon                      = null;

        private     Texture2D               m_toggleOnIcon                  = null;

        private     Texture2D               m_toggleOffIcon                 = null;

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            // set properties
            m_propertyMetaArray     = new PropertyGroupMeta[]
            {
                new PropertyGroupMeta() { displayName = "Barcode Scanner",              serializedPropertyName = "m_barcodeScannerSettings",           onAfterPropertyDraw = DrawBarcodeScannerControlSettings},
                new PropertyGroupMeta() { displayName = "Object Detector and Tracker",  serializedPropertyName = "m_objectDetectorAndTrackerSettings",           onAfterPropertyDraw = DrawObjectDetectorAndScannerControlSettings },
                new PropertyGroupMeta() { displayName = "Text Recognizer",              serializedPropertyName = "m_textRecognizerSettings",           onAfterPropertyDraw = DrawTextRecognizerControlSettings },
                new PropertyGroupMeta() { displayName = "Digital Ink Recognizer",       serializedPropertyName = "m_digitalInkRecognizerSettings",           onAfterPropertyDraw = DrawDigitalInkRecognizerControlSettings },

            };
            m_properties            = Array.ConvertAll(m_propertyMetaArray, (element) => serializedObject.FindProperty(element.serializedPropertyName));
            m_propertyCount         = m_properties.Length;
            m_formattedVersion      = string.Format("v{0}", EasyMLKitSettings.Version);

            // load assets

            var resourcesPath       = EasyMLKitPackageLayout.EditorResourcesPath;
            m_logoIcon              = AssetDatabase.LoadAssetAtPath<Texture2D>(resourcesPath + "/Textures/logo.png");
            m_toggleOnIcon          = AssetDatabase.LoadAssetAtPath<Texture2D>(resourcesPath + "/Textures/toggle-on.png");
            m_toggleOffIcon         = AssetDatabase.LoadAssetAtPath<Texture2D>(resourcesPath + "/Textures/toggle-off.png");
        }

        public override void OnInspectorGUI()
        {
            LoadStyles();

            // draw controls
            DrawProductInfoSection();
            DrawTopBarButtons();
            EditorGUI.BeginChangeCheck();
            for (int iter = 0; iter < m_propertyCount; iter++)
            {
                var     property    = m_properties[iter];
                if (property != null)
                {
                    var     propertyMeta    = m_propertyMetaArray[iter];
                    DrawPropertyGroup(property, propertyMeta);
                }
            }
            GUILayout.Space(5f);

            // save changes
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();
            }
        }

        #endregion

        #region Section methods

        private void DrawProductInfoSection()
        {
            GUILayout.BeginHorizontal(m_groupBackgroundStyle);

            // logo section
            GUILayout.BeginVertical();
            GUILayout.Space(2f);
            GUILayout.Label(m_logoIcon, GUILayout.Height(64f), GUILayout.Width(64f));
            GUILayout.Space(2f);
            GUILayout.EndVertical();

            // product info
            GUILayout.BeginVertical();
            GUILayout.Label(EasyMLKitSettings.DisplayName, "HeaderLabel");
            GUILayout.Label(m_formattedVersion, "MiniLabel");
            GUILayout.Label("Copyright © 2022 Voxel Busters Interactive LLP.", "MiniLabel");
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DrawTopBarButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Documentation", "ButtonLeft"))
            {
                ProductResources.OpenDocumentation();
            }
            if (GUILayout.Button("Tutorials", "ButtonMid"))
            {
                ProductResources.OpenTutorials();
            }
            if (GUILayout.Button("Forum", "ButtonMid"))
            {
                ProductResources.OpenForum();
            }
            if (GUILayout.Button("Discord", "ButtonMid"))
            {
                ProductResources.OpenSupport();
            }
            if (GUILayout.Button("Write Review", "ButtonRight"))
            {
                ProductResources.OpenAssetStorePage(true);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DrawPropertyGroup(SerializedProperty property, PropertyGroupMeta propertyMeta)
        {
            EditorGUILayout.BeginVertical(m_groupBackgroundStyle);
            if (DrawControlHeader(property, propertyMeta.displayName))
            {
                bool    oldGUIState         = GUI.enabled;
                var     enabledProperty     = property.FindPropertyRelative("m_isEnabled");

                // update gui state
                GUI.enabled     = (enabledProperty == null || enabledProperty.boolValue);

                // show internal properties
                EditorGUI.indentLevel++;
                if (enabledProperty != null)
                {
                    DrawSettingsInternalProperties(property);
                }
                else
                {
                    DrawControlInternalProperties(property);
                }
                if (propertyMeta.onAfterPropertyDraw != null)
                {
                    propertyMeta.onAfterPropertyDraw();
                }
                EditorGUI.indentLevel--;

                // reset gui state
                GUI.enabled     = oldGUIState;
            }
            EditorGUILayout.EndVertical();
        }

        private bool DrawControlHeader(SerializedProperty property, string displayName)
        {
            // draw rect
            var     rect                = EditorGUILayout.GetControlRect(false, 30f);
            GUI.Box(rect, GUIContent.none, m_headerStyle);

            // draw foldable control
            bool    isSelected          = property == m_activeProperty;
            var     foldOutRect         = new Rect(rect.x + 10f, rect.y, 50f, rect.height);
            EditorGUI.LabelField(foldOutRect, isSelected ? "-" : "+", m_headerFoldoutStyle);

            // draw label 
            var     labelRect           = new Rect(rect.x + 25f, rect.y, rect.width - 100f, rect.height);
            EditorGUI.LabelField(labelRect, displayName, m_headerLabelStyle);

            // draw selectable rect
            var     selectableRect      = new Rect(rect.x, rect.y, rect.width - 100f, rect.height);
            if (DrawTransparentButton(selectableRect, string.Empty))
            {
                isSelected              = OnPropertyHeaderSelect(property);
            }

            // draw toggle button
            var     enabledProperty     = property.FindPropertyRelative("m_isEnabled");
            if ((enabledProperty != null) && SettingsPropertyGroup.CanToggleFeatureUsageState())
            {
                Vector2 iconSize = new Vector2(64f, 24f);
                Rect toggleRect = new Rect(rect.xMax - (iconSize.x * 1.1f), rect.y + (rect.height / 2 - iconSize.y / 2), iconSize.x , iconSize.y);

                if (GUI.Button(toggleRect, enabledProperty.boolValue ? m_toggleOnIcon : m_toggleOffIcon, m_headerToggleStyle))
                {
                    enabledProperty.boolValue       = !enabledProperty.boolValue;
                }
                
            }
            return isSelected;
        }

        private static void DrawControlInternalProperties(SerializedProperty property)
        {
            // move pointer to first element
            var     currentProperty  = property.Copy();
            var     endProperty      = default(SerializedProperty);

            // start iterating through the properties
            bool    firstTime   = true;
            while (currentProperty.NextVisible(enterChildren: firstTime))
            {
                if (firstTime)
                {
                    endProperty = property.GetEndProperty();
                    firstTime   = false;
                }
                if (SerializedProperty.EqualContents(currentProperty, endProperty))
                {
                    break;
                }
                EditorGUILayout.PropertyField(currentProperty, true);
            }
        }

        private bool OnPropertyHeaderSelect(SerializedProperty property)
        {
            var     oldProperty     = m_activeProperty;
            if (m_activeProperty == null)
            {
                property.isExpanded = true;

                m_activeProperty    = property;

                return true;
            }
            if (m_activeProperty == property)
            {
                property.isExpanded = false;

                m_activeProperty    = null;

                return false;
            }

            property.isExpanded     = true;
            oldProperty.isExpanded  = false;
            
            m_activeProperty        = property;

            return true;
        }

        #endregion

        #region Settings group methods

        private static void DrawSettingsInternalProperties(SerializedProperty settingsProperty)
        {
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 300f;

            // move pointer to first element
            var     currentProperty  = settingsProperty.Copy();
            currentProperty.NextVisible(enterChildren: true);
            var     endProperty      = settingsProperty.GetEndProperty();

            // start iterating through the properties
            while (currentProperty.NextVisible(enterChildren: false))
            {
                if (SerializedProperty.EqualContents(currentProperty, endProperty))
                {
                    break;
                }
                EditorGUILayout.PropertyField(currentProperty, true) ;
            }

            // reset label width setting
            EditorGUIUtility.labelWidth = oldLabelWidth;
        }

        #endregion

        #region Features methods

        private void DrawBarcodeScannerControlSettings()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Resource Page"))
            {
                ProductResources.OpenResourcePage(NativeFeatureType.kBarcodeScanner);
            }
            GUILayout.EndVertical();
        }

        private void DrawObjectDetectorAndScannerControlSettings()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Resource Page"))
            {
                ProductResources.OpenResourcePage(NativeFeatureType.kObjectDetectorAndTracker);
            }
            GUILayout.EndVertical();
        }

        private void DrawTextRecognizerControlSettings()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Resource Page"))
            {
                ProductResources.OpenResourcePage(NativeFeatureType.kTextRecognizer);
            }
            GUILayout.EndVertical();
        }

        private void DrawDigitalInkRecognizerControlSettings()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("Resource Page"))
            {
                ProductResources.OpenResourcePage(NativeFeatureType.kDigitalInkRecognizer);
            }
            GUILayout.EndVertical();
        }

        #endregion

        #region GUIStyles methods

        private void LoadStyles()
        {
            // check whether styles are already loaded
            if (null != m_groupBackgroundStyle)
            {
                return;
            }

            // bg style
            m_groupBackgroundStyle          = new GUIStyle("HelpBox");
            var     bgOffset                = m_groupBackgroundStyle.margin;
            bgOffset.bottom                 = 5;
            m_groupBackgroundStyle.margin   = bgOffset;

            // header style
            m_headerStyle                   = new GUIStyle("PreButton");
            m_headerStyle.fixedHeight       = 0;

            // foldout style
            m_headerFoldoutStyle            = EditorStyles.boldLabel;
            m_headerFoldoutStyle.fontSize   = 20;
            m_headerFoldoutStyle.alignment  = TextAnchor.MiddleLeft;

            // label style
            m_headerLabelStyle              = EditorStyles.boldLabel;
            m_headerLabelStyle.fontSize     = 12;
            m_headerLabelStyle.alignment    = TextAnchor.MiddleLeft;

            // enabled style
            m_headerToggleStyle             = new GUIStyle("InvisibleButton");
        }

        private bool DrawTransparentButton(Rect rect, string label)
        {
            var     originalColor   = GUI.color;
            try
            {
                GUI.color   = Color.clear;
                return GUI.Button(rect, string.Empty);
            }
            finally
            {
                GUI.color   = originalColor;
            }
        }

        #endregion

        #region Nested types

        private struct PropertyGroupMeta
        {
            public  string      serializedPropertyName;

            public  string      displayName;

            public  Action      onAfterPropertyDraw;
        }

        #endregion
    }
}
#endif