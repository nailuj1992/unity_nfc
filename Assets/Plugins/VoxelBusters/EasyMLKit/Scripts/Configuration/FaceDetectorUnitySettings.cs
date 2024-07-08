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
    public class FaceDetectorUnitySettings : SettingsPropertyGroup
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public FaceDetectorUnitySettings( bool enabled = true) : base(NativeFeatureType.kFaceDetector, enabled)
        {
        }

        #endregion
    }
}