using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// This creates live camera as input source
    /// </summary>
    public class LiveCameraInputSource : IImageInputSource
    {
        /// <summary>
        /// Enable this to switch on the flash
        /// </summary>
        public bool EnableFlash
        {
            get; set;
        }

        /// <summary>
        /// Set this to on for enabling front facing camera
        /// </summary>
        public bool IsFrontFacing
        {
            get; set;
        }

        public LiveCameraInputSource()
        {
            IsFrontFacing = false;
            EnableFlash = false;
        }

        public void Close()
        {
        }

        public float GetWidth()
        {
            return Screen.width;
        }

        public float GetHeight()
        {
            return Screen.height;
        }
    }
}
