using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class TextGroup
    {
        public class Line
        {
            public string Text
            {
                get;
                private set;
            }

            public List<Element> Elements
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

            public Line(string text, List<Element> elements, string recognizedLanguage, Rect boundingBox)
            {
                Text = text;
                Elements = elements;
                RecognizedLanguage = recognizedLanguage;
                BoundingBox = boundingBox;
            }
        }
    }
}