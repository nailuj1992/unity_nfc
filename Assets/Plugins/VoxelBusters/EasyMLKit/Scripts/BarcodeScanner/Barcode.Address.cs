using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class Address
        {
            public enum AddressType
            {
                Unknown,
                Home,
                Work
            }

            public List<string> AddressLines
            {
                get;
                private set;
            }

            public AddressType Type
            {
                get;
                private set;
            }

            public Address(AddressType type, string[] addressLines)
            {
                Type                = type;
                AddressLines        = new List<string>(addressLines);
            }
        }
    }
}