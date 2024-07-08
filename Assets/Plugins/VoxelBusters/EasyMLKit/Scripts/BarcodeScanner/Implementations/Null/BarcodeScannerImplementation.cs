using VoxelBusters.CoreLibrary;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Null
{
    public class BarcodeScannerImplementation : BarcodeScannerImplementationBase
    {
        private string NotAvailable = "Not available on this platform";
        public BarcodeScannerImplementation() : base(isAvailable: true)
        {
        }

        public override void Prepare(IImageInputSource inputSource, BarcodeScannerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAvailable));
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<BarcodeScannerResult> callback)
        {
            if (callback != null)
            {
                callback(new BarcodeScannerResult(null, new Error(NotAvailable)));
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAvailable));
            }
        }

    }
}