#if UNITY_EDITOR
using System.Collections.Generic;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Simulator
{
    public partial class DigitalInkRecognizerImplementation : DigitalInkRecognizerImplementationBase
    {
        public DigitalInkRecognizerImplementation() : base(isAvailable: true)
        {
        }

        public override void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> callback)
        {
            if (callback != null)
            {
                callback(CreateDummyResult());
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }

        private DigitalInkRecognizerResult CreateDummyResult()
        {
            List<DigitalInkRecognizedValue> values = new List<DigitalInkRecognizedValue>();
            values.Add(new DigitalInkRecognizedValue("Dummy", 1f));
            DigitalInkRecognizerResult result = new DigitalInkRecognizerResult(values, null);

            return result;
        }

        public override DigitalInkRecognizerModelManager GetModelManager()
        {
            return new ModelManager();
        }
    }
}
#endif