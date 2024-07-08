using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    public abstract class DigitalInkRecognizerModelManager
    {
        public abstract bool IsModelAvailable(DigitalInkRecognizerModelIdentifier modelIdentifier);
        public abstract void DownloadModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback listener);
        public abstract void DeleteModel(DigitalInkRecognizerModelIdentifier modelIdentifier, OnCompleteCallback listener);
    }
}