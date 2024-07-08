using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class TextGroup
    {
        public class Element
        {
            public string Text
            {
                get;
                private set;
            }

            public string RecognizedLanguage
            {
                get;
                private set;
            }

            public Rect BoundingBox
            {
                get;
                private set;
            }

            public Element(string text, string recognizedLanguage, Rect boundingBox)
            {
                Text = text;
                RecognizedLanguage = recognizedLanguage;
                BoundingBox = boundingBox;
            }
        }
    }
}