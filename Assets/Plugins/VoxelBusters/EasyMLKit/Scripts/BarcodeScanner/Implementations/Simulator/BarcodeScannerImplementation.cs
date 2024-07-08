#if UNITY_EDITOR
using System.Collections.Generic;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Simulator
{
    public class BarcodeScannerImplementation : BarcodeScannerImplementationBase
    {
        public BarcodeScannerImplementation() : base(isAvailable: true)
        {
        }

        public override void Prepare(IImageInputSource inputSource, BarcodeScannerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<BarcodeScannerResult> callback)
        {
            if (callback != null)
            {
                Barcode barcode = new Barcode(BarcodeFormat.AZTEC, BarcodeValueType.UNKNOWN, "testing", "test", null, "Display test", new UnityEngine.Rect(37,37, 950, 950));

                callback(new BarcodeScannerResult(new List<Barcode>(){barcode, barcode }, null));
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }
    }
}
#endif