using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Barcode Formats to scan from the input source
    /// </summary>
    public enum BarcodeFormat
    {
        UNKNOWN = -1,
        ALL = 0,
        CODE_128 = 1 << 0,
        CODE_39 = 1 << 1,
        CODE_93 = 1 << 2,
        CODABAR = 1 << 3,
        DATA_MATRIX = 1 << 4,
        EAN_13 = 1 << 5,
        EAN_8 = 1 << 6,
        ITF = 1 << 7,
        QR_CODE = 1 << 8,
        UPC_A = 1 << 9,
        UPC_E = 1 << 10,
        PDF417 = 1 << 11,
        AZTEC = 1 << 12
    }
}