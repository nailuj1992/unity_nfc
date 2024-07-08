using System.Collections.Generic;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Result delivered once processing is done for an input instance.
    /// </summary>
    public class BarcodeScannerResult
    {
        /// <summary>
        /// List of barcodes identified
        /// </summary>
        public List<Barcode> Barcodes
        {
            get;
            private set;
        }


        /// <summary>
        /// Error if any, while scanning
        /// </summary>
        public Error Error
        {
            get;
            private set;
        }

        public BarcodeScannerResult(List<Barcode> barcodes, Error error)
        {
            Barcodes = barcodes;
            Error = error;
        }

        /// <summary>
        /// Returns true if there is an error
        /// </summary>
        /// <returns></returns>
        public bool HasError()
        {
            return Error != null;
        }
    }
}