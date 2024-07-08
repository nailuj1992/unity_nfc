using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public class PassportVerificationModel : IPassportVerificationModel
    {
        public string MRZString { get; set; }

        private PassportScan PassportScan { get; }

        public PassportVerificationModel()
        {
            PassportScan = new PassportScan();
        }

        public async Task Recognize(CancellationToken cancellationToken, ITextureGetter textureGetter = null)
        {
            MRZString = await PassportScan.Recognize(cancellationToken, textureGetter);
        }
    }
}
