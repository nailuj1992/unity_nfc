using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class Barcode
    {
        public class Wifi
        {
            public enum WifiEncryptionType
            {
                Open,
                Wep,
                Wpa
            }
            public WifiEncryptionType EncryptionType
            {
                get;
                private set;
            }

            public string Password
            {
                get;
                private set;
            }

            public string Ssid
            {
                get;
                private set;
            }

            public Wifi(WifiEncryptionType encryptionType, string password, string ssid)
            {
                EncryptionType  = encryptionType;
                Password        = password;
                Ssid            = ssid;
            }
        }
    }
}