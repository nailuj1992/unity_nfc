using VoxelBusters.CoreLibrary;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Null
{
    public class TextRecognizerImplementation : TextRecognizerImplementationBase
    {
        private string NotAvailable = "Not available on this platform";
        public TextRecognizerImplementation() : base(isAvailable: false)
        {
        }

        public override void Prepare(IImageInputSource inputSource, TextRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAvailable));
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<TextRecognizerResult> callback)
        {
            if (callback != null)
            {
                callback(new TextRecognizerResult(null, new Error(NotAvailable)));
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