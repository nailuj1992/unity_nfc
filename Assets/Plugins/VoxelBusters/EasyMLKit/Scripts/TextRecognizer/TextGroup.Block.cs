using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class TextGroup
    {
        public class Block
        {
            public string Text
            {
                get;
                private set;
            }

            public List<Line> Lines
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

            public Block(string text, List<Line> lines, string recognizedLanguage, Rect boundingBox)
            {
                Text = text;
                Lines = lines;
                RecognizedLanguage = recognizedLanguage;
                BoundingBox = boundingBox;
            }
        }
    }
}