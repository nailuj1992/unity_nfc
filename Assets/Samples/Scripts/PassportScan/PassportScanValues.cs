using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public class PassportScanValues
    {
        public IPassportVerificationModel Model { get; }

        private static PassportScanValues instance = null;

        private PassportScanValues()
        {
            Model = new PassportVerificationModel();
        }

        public static PassportScanValues GetInstance()
        {
            if (instance == null)
            {
                instance = new PassportScanValues();
            }
            return instance;
        }
    }
}
