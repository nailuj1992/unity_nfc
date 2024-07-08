using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    [Serializable]
    public class DigitalInkRecognizerUnitySettings : SettingsPropertyGroup
    {
        #region Fields
        #endregion

        #region Properties
        
        #endregion

        #region Constructors

        public DigitalInkRecognizerUnitySettings( bool enabled = true) : base(NativeFeatureType.kDigitalInkRecognizer, enabled)
        {
        }

        #endregion
    }
}