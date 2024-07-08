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
    internal class DigitalInkRecognizerResultParser
    {
        NativeArray m_nativeValues;

        public DigitalInkRecognizerResultParser(NativeArray nativeValues)
        {
            m_nativeValues = nativeValues;
        }

        public List<DigitalInkRecognizedValue> GetResult()
        {
            return GetValues(m_nativeValues.GetStructArray<NativeDigitalInkRecognizedValue>());
        }

        private List<DigitalInkRecognizedValue> GetValues(NativeDigitalInkRecognizedValue[] nativeValues)
        {
            List<DigitalInkRecognizedValue> values = new List<DigitalInkRecognizedValue>();

            foreach(NativeDigitalInkRecognizedValue nativeValue in nativeValues)
            {
                float? score = null;

                if(nativeValue.ScoreExists == 1)
                {
                    score = nativeValue.Score;
                }

                DigitalInkRecognizedValue eachValue = new DigitalInkRecognizedValue(nativeValue.Text.AsString(), score);
                values.Add(eachValue);
            }

            return values;

        }
    }
}
#endif
