using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class GeoPoint
        {
            public double Latitude
            {
                get;
                private set;
            }

            public double Longitude
            {
                get;
                private set;
            }

            public GeoPoint(double latitude, double longitude)
            {
                Latitude    = latitude;
                Longitude   = longitude;
            }
        }
    }
}