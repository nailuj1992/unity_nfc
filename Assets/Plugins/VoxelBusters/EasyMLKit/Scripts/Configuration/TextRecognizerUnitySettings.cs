using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    [Serializable]
    public class TextRecognizerUnitySettings : SettingsPropertyGroup
    {
        #region Fields
        [SerializeField]
        [Tooltip("Enable this to pass chinese languages as input to TextRecognizer")]
        private bool m_needsChineseLanguagesRecognition;

        [SerializeField]
        [Tooltip("Enable this to pass devanagari languages as input to TextRecognizer")]
        private bool m_needsDevanagariLanguagesRecognition;

        [SerializeField]
        [Tooltip("Enable this to pass japanese languages as input to TextRecognizer")]
        private bool m_needsJapaneseLanguagesRecognition;

        [SerializeField]
        [Tooltip("Enable this to pass korean languages as input to TextRecognizer")]
        private bool m_needsKoreanLanguagesRecognition;

        #endregion

        #region Properties
        public bool NeedsChineseLanguagesRecognition
        {
            get
            {
                return m_needsChineseLanguagesRecognition;
            }
        }

        [SerializeField]

        public bool NeedsDevanagariLanguagesRecognition
        {
            get
            {
                return m_needsDevanagariLanguagesRecognition;
            }
        }

        public bool NeedsJapaneseLanguagesRecognition
        {
            get
            {
                return m_needsJapaneseLanguagesRecognition;
            }
            
        }

        public bool NeedsKoreanLanguagesRecognition
        {
            get
            {
                return m_needsKoreanLanguagesRecognition;
            }
        }

        #endregion

        #region Constructors

        public TextRecognizerUnitySettings( bool enabled = true,
                                            bool needsChineseLanguagesRecognition = false,
                                            bool needsDevanagariLanguagesRecognition = false,
                                            bool needsJapaneseLanguagesRecognition = false,
                                            bool needsKoreanLanguagesRecognition = false) : base(NativeFeatureType.kTextRecognizer, enabled)
        {
            m_needsChineseLanguagesRecognition = needsChineseLanguagesRecognition;
            m_needsDevanagariLanguagesRecognition = needsDevanagariLanguagesRecognition;
            m_needsJapaneseLanguagesRecognition = needsJapaneseLanguagesRecognition;
            m_needsKoreanLanguagesRecognition = needsKoreanLanguagesRecognition;
        }

        #endregion
    }
}