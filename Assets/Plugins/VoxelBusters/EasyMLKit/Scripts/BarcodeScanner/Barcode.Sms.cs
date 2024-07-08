using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class Sms
        {
            public string Message
            {
                get;
                private set;
            }

            public string PhoneNumber
            {
                get;
                private set;
            }

            public Sms(string message, string phoneNumber)
            {
                Message         = message;
                PhoneNumber     = phoneNumber;
            }
        }
    }
}