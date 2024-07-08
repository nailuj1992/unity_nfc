using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class UrlBookmark
        {
            public string Title
            {
                get;
                private set;
            }

            public string Url
            {
                get;
                private set;
            }

            public UrlBookmark(string title, string url)
            {
                Title   = title;
                Url     = url;
            }
        }
    }
}