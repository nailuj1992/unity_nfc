using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public class DigitalInkRecognizerModelIdentifier
    {
        private string m_identifier;

        internal string GetIdentifier()
        {
            return m_identifier;
        }

        public static readonly DigitalInkRecognizerModelIdentifier English = new DigitalInkRecognizerModelIdentifier("en");
        public static readonly DigitalInkRecognizerModelIdentifier Spanish = new DigitalInkRecognizerModelIdentifier("es");
        public static readonly DigitalInkRecognizerModelIdentifier French  = new DigitalInkRecognizerModelIdentifier("fr");
        public static readonly DigitalInkRecognizerModelIdentifier AUTODRAW = new DigitalInkRecognizerModelIdentifier("zxx-Zsym-x-autodraw");


        private DigitalInkRecognizerModelIdentifier(string identifier)
        {
            m_identifier = identifier;
        }

    }
}