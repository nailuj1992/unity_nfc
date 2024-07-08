using VoxelBusters.CoreLibrary;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Null
{
    public partial class DigitalInkRecognizerImplementation : DigitalInkRecognizerImplementationBase
    {
        private static string NotAvailable = "Not available on this platform";
        public DigitalInkRecognizerImplementation() : base(isAvailable: false)
        {
        }

        public override void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAvailable));
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> callback)
        {
            if (callback != null)
            {
                callback(new DigitalInkRecognizerResult(null, new Error(NotAvailable)));
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(new Error(NotAvailable));
            }
        }

        public override DigitalInkRecognizerModelManager GetModelManager()
        {
            return new ModelManager();
        }
    }
}