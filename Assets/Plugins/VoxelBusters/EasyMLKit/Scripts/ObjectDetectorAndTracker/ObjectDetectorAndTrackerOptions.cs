using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    public class ObjectDetectorAndTrackerOptions
    {
        public bool EnableClassification
        {
            get;
            private set;
        }

        public bool EnableMultipleObjectDetection
        {
            get;
            private set;
        }

        public float ClassificationConfidenceThreshold
        {
            get;
            private set;
        }

        public URLString? CustomModelPath
        {
            get;
            private set;
        }

        private ObjectDetectorAndTrackerOptions()
        {
            EnableClassification = false;
            EnableMultipleObjectDetection = false;
            ClassificationConfidenceThreshold = 0.0f;
            CustomModelPath = null;
        }


        public class Builder
        {
            ObjectDetectorAndTrackerOptions options;
            public Builder()
            {
                options = new ObjectDetectorAndTrackerOptions();
            }

            public Builder EnableClassification(bool enable)
            {
                options.EnableClassification = enable;
                return this;
            }

            public Builder EnableMultipleObjectDetection(bool enable)
            {
                options.EnableMultipleObjectDetection = enable;
                return this;
            }

            public Builder SetClassificationConfidenceThreshold(float threshold)
            {
                options.ClassificationConfidenceThreshold = threshold;
                return this;
            }

            public Builder SetCustomModelPath(URLString? path)
            {
                options.CustomModelPath = path;
                return this;
            }

            public ObjectDetectorAndTrackerOptions Build()
            {
                return options;
            }
        }
    }
}
