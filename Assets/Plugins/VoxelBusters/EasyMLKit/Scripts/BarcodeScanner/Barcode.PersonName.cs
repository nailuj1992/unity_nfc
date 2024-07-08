using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class PersonName
        {
            public string First
            {
                get;
                private set;
            }

            public string Middle
            {
                get;
                private set;
            }

            public string Last
            {
                get;
                private set;
            }

            public string Prefix
            {
                get;
                private set;
            }

            public string Pronunciation
            {
                get;
                private set;
            }

            public string Suffix
            {
                get;
                private set;
            }

            public string FormattedName
            {
                get;
                private set;
            }

            public PersonName(string first, string middle, string last, string prefix, string suffix, string pronunciation, string formattedName)
            {
                First           = first;
                Middle          = middle;
                Last            = last;
                Prefix          = prefix;
                Suffix          = suffix;
                Pronunciation   = pronunciation;
                FormattedName   = formattedName;
            }
        }
    }
}