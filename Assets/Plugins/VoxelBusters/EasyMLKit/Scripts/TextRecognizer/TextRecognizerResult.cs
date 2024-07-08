using System.Collections.Generic;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Result delivered once processing is done for an input instance.
    /// </summary>
    public class TextRecognizerResult
    {
        /// <summary>
        /// Recognized text data
        /// </summary>
        public TextGroup TextGroup
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

        public TextRecognizerResult(TextGroup textGroup, Error error)
        {
            TextGroup = textGroup;
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