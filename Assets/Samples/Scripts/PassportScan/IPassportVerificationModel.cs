using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public interface IPassportVerificationModel
    {
        string MRZString { get; set; }

        Task Recognize(CancellationToken cancellationToken, ITextureGetter textureGetter = null);
    }
}
