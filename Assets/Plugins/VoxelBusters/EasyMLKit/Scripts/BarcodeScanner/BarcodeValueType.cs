using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VoxelBusters.EasyMLKit
{
    public enum BarcodeValueType
    {
        UNKNOWN = 0,
        CONTACT_INFO,
        EMAIL,
        ISBN,
        PHONE,
        PRODUCT,
        SMS,
        TEXT,
        URL,
        WIFI,
        GEO,
        CALENDAR_EVENT,
        DRIVER_LICENSE
    }
}