using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class DrawingInputSource : IDrawingInputSource
    {


        private float m_drawingAreaWidth;
        private float m_drawingAreaHeight;
        private List<DrawingStroke> m_strokes = new List<DrawingStroke>();

        public DrawingInputSource(float drawingAreaWidth, float drawingAreaHeight)
        {
            m_drawingAreaWidth = drawingAreaWidth;
            m_drawingAreaHeight = drawingAreaHeight;
        }

        public void AddStroke(DrawingStroke stroke)
        {
            m_strokes.Add(stroke);
        }

        public DrawingStroke[] GetStrokes()
        {
            return m_strokes.ToArray();
        }


        public float GetHeight()
        {
            return m_drawingAreaWidth;
        }

        public float GetWidth()
        {
            return m_drawingAreaHeight;
        }
    }
}