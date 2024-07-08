using System.Collections.Generic;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Result delivered once processing is done for an input instance.
    /// </summary>
    public class DigitalInkRecognizerResult
    {
        /// <summary>
        /// Recognized values data
        /// </summary>
        public List<DigitalInkRecognizedValue> Values
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

        public DigitalInkRecognizerResult(List<DigitalInkRecognizedValue> values, Error error)
        {
            Values = values;
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