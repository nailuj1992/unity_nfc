#if UNITY_IOS
using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Implementations.iOS.Internal;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.iOS
{
    internal class TextRecognizerResultParser
    {
        float m_displayWidth;
        float m_displayHeight;
        float m_inputWidth;
        float m_inputHeight;
        float m_inputRotation;
        NativeText m_nativeText;

        public TextRecognizerResultParser(NativeText text, float displayWidth, float displayHeight, float inputWidth, float inputHeight, float inputRotation)
        {
            m_nativeText = text;
            m_displayWidth = displayWidth;
            m_displayHeight = displayHeight;
            m_inputWidth = inputWidth;
            m_inputHeight = inputHeight;
            m_inputRotation = inputRotation;
        }

        public TextGroup GetResult()
        {
            return GetTextGroup(m_nativeText);
        }

        private TextGroup GetTextGroup(NativeText nativeText)
        {
            if (nativeText.RawValue != IntPtr.Zero)
            {
                return new TextGroup(nativeText.RawValue.AsString(), GetTextBlocks(nativeText.Blocks));
            }
            else
            {
                return null;
            }

        }

        private List<TextGroup.Block> GetTextBlocks(NativeArray array)
        {
            if (array.Length == 0)
                return null;
  
            NativeTextBlock[] nativeTextBlocks = array.GetStructArray<NativeTextBlock>();
            List<TextGroup.Block> blocks = new List<TextGroup.Block>();
            foreach (NativeTextBlock each in nativeTextBlocks)
            {
                var lines = GetTextLines(each.Lines);
                TextGroup.Block block = new TextGroup.Block(each.DisplayValue.AsString(), lines, "", GetRect(each.Frame));
                blocks.Add(block);
            }

            return blocks;
        }

        private List<TextGroup.Line> GetTextLines(NativeArray array)
        {
            NativeTextLine[] nativeTextLines = array.GetStructArray<NativeTextLine>();
            List<TextGroup.Line> lines = new List<TextGroup.Line>();
            foreach (NativeTextLine each in nativeTextLines)
            {
                var elements = GetTextElements(each.Elements);
                TextGroup.Line line = new TextGroup.Line(each.DisplayValue.AsString(), elements, "", GetRect(each.Frame));
                lines.Add(line);
            }

            return lines;
        }

        private List<TextGroup.Element> GetTextElements(NativeArray array)
        {
            NativeTextElement[] nativeTextElements = array.GetStructArray<NativeTextElement>();
            List<TextGroup.Element> elements = new List<TextGroup.Element>();
            foreach (NativeTextElement each in nativeTextElements)
            {
                TextGroup.Element element = new TextGroup.Element(each.DisplayValue.AsString(), "", GetRect(each.Frame));
                elements.Add(element);
            }

            return elements;
        }

        private Rect GetRect(UnityRect nativeRect)
        {   
            Rect rect = new Rect(nativeRect.X, nativeRect.Y, nativeRect.Width, nativeRect.Height);
            Rect transformedRect = InputSourceUtility.TransformRectToUserSpace(rect, m_displayWidth, m_displayHeight, m_inputWidth, m_inputHeight, m_inputRotation);
            return transformedRect;
        }
    }
}
#endif