using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class Phone
        {
            public enum PhoneType
            {
                Unknown,
                Fax,
                Home,
                Mobile,
                Work
            }

            public PhoneType Type
            {
                get;
                private set;
            }

            public string Number
            {
                get;
                private set;
            }

            public Phone(PhoneType type, string number)
            {
                Type    = type;
                Number  = number;
            }
        }
    }
}