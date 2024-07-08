using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public class DetectedObject
    {
        public int? TrackingId
        {
            get;
            private set;
        }

        public Rect BoundingBox
        {
            get;
            private set;
        }

        public List<Label> Labels
        {
            get;
            private set;
        }

        public DetectedObject(Rect boundingBox, List<Label> labels, int? trackingId)
        {
            BoundingBox = boundingBox;
            Labels = labels;
            TrackingId = trackingId;
        }

        public class Label
        {
            public int Index
            {
                get;
                private set;
            }

            public string Text
            {
                get;
                private set;
            }

            public float Confidence
            {
                get;
                private set;
            }

            public Label(int index, string text, float confidence)
            {
                Index = index;
                Text = text;
                Confidence = confidence;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        public override string ToString()
        {
            return string.Format("Tracking Id : {0}  Labels : {1} Bounding Box : {2}", TrackingId, string.Join(",", Labels), BoundingBox);
        }
    }
}
