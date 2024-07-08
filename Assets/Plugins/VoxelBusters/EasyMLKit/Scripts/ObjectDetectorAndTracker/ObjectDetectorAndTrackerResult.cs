using System.Collections.Generic;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    public class ObjectDetectorAndTrackerResult
    {
        public List<DetectedObject> DetectedObjects
        {
            get;
            private set;
        }

        public Error Error
        {
            get;
            private set;
        }

        public ObjectDetectorAndTrackerResult(List<DetectedObject> detectedObjects, Error error)
        {
            DetectedObjects = detectedObjects;
            Error = error;
        }

        public bool HasError()
        {
            return Error != null;
        }
    }
}