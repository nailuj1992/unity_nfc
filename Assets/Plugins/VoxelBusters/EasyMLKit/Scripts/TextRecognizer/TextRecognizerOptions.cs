using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Options required to set before recognizing text.
    /// </summary>
    public class TextRecognizerOptions
    {
        public TextRecognizerInputLanguage InputLanguage
        {
            get;
            private set;
        }

        private TextRecognizerOptions(TextRecognizerInputLanguage language)
        {
            InputLanguage = language;
        }


        public class Builder
        {
            TextRecognizerInputLanguage m_language;
            public Builder()
            {
            }

            public void SetInputLanguage(TextRecognizerInputLanguage language)
            {
                m_language = language;
            }

            public TextRecognizerOptions Build()
            {
                TextRecognizerOptions options = new TextRecognizerOptions(m_language);
                return options;
            }
        }
    }
}
