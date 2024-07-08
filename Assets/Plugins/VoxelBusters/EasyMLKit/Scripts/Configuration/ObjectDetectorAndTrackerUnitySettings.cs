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
    public class ObjectDetectorAndTrackerUnitySettings : SettingsPropertyGroup
    {
        #region Fields


        #endregion

        #region Properties


        #endregion

        #region Constructors

        public ObjectDetectorAndTrackerUnitySettings(bool enabled = true) : base(NativeFeatureType.kObjectDetectorAndTracker, enabled)
        { 
        }

        #endregion
    }
}