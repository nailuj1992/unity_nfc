using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Options required to set before recognizing text.
    /// </summary>
    public class DigitalInkRecognizerOptions
    {
        public DigitalInkRecognizerModelIdentifier ModelIdentifier { get; private set; }
        public float Width { get; private set; }
        public float Height { get; private set; }
        public string PreContext { get; private set; }

        private DigitalInkRecognizerOptions()
        {
            ModelIdentifier = DigitalInkRecognizerModelIdentifier.English; //In docs mention, default is english.
            Width = 0f;
            Height = 0f;
            PreContext = null;
        }


        public class Builder
        {
            private DigitalInkRecognizerOptions m_options;

            public Builder()
            {
                m_options = new DigitalInkRecognizerOptions();
            }

            public void SetModelIdentifier(DigitalInkRecognizerModelIdentifier identifier)
            {
                m_options.ModelIdentifier = identifier;
            }

            public void SetWidth(float width)
            {
                m_options.Width = width;
            }

            public void SetHeight(float height)
            {
                m_options.Height = height;
            }


            public void SetPreContext(string preContext)
            {
                m_options.PreContext = preContext;
            }


            public DigitalInkRecognizerOptions Build()
            {
                return m_options;
            }
        }
    }
}
