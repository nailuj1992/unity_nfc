using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Options required to set before scanning barcodes.
    /// </summary>
    public class BarcodeScannerOptions
    {
        /// <summary>
        /// Allowed formats for scanning. If you set these formats list to minimal, the processing will be faster.
        /// </summary>
        public BarcodeFormat ScannableFormats
        {
            get;
            private set;
        }

        private BarcodeScannerOptions()
        {
        }


        public class Builder
        {
            private     BarcodeFormat formats = BarcodeFormat.ALL;

            public Builder()
            {
            }

            public Builder SetScannableFormats(BarcodeFormat formats)
            {
                this.formats = formats;
                return this;
            }

            public BarcodeScannerOptions Build()
            {
                BarcodeScannerOptions options = new BarcodeScannerOptions();
                options.ScannableFormats = formats;
                return options;
            }
        }
    }
}
