#if UNITY_IOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;

namespace VoxelBusters.EasyMLKit.Implementations.iOS.Internal
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeDigitalInkRecognizedValue
    {
        public IntPtr Text;
        public float Score;
        public int ScoreExists;
    };


    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeDrawingStrokePoint
    {
        public float    X;
        public float    Y;
        public long     Timestamp;

        public NativeDrawingStrokePoint(float x, float y, long timestamp)
        {
            X = x;
            Y = y;
            Timestamp = timestamp;
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeDrawingStroke
    {
        public NativeDrawingStrokePoint[] Points;
        public int Count;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeDrawing
    {
        public NativeDrawingStroke[] Strokes;
        public int Count;
    };

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeDigitalInkRecognizerOptions
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string ModelIdentifier;
        public float InputWidth;
        public float InputHeight;
        [MarshalAs(UnmanagedType.LPStr)]
        public string PreContext;

        public NativeDigitalInkRecognizerOptions(string identifier, float width, float height, string preContext) : this()
        {
            ModelIdentifier = identifier;
            InputWidth = width;
            InputHeight = height;
            PreContext = preContext;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct NativeDigitalInkRecognizerModelIdentifier
    {
        [MarshalAs(UnmanagedType.LPStr)]
        public string Identifier;

        public NativeDigitalInkRecognizerModelIdentifier(string identifier) : this()
        {
            Identifier = identifier;
        }
    }

}
#endif