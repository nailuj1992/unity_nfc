using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit.Demo
{
    public interface IDrawingStrokeListener
    {
        void OnNewStroke(DrawingStroke stroke);
    }
}