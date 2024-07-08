#if UNITY_EDITOR
using System.Collections.Generic;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.Simulator
{
    public class TextRecognizerImplementation : TextRecognizerImplementationBase
    {
        public TextRecognizerImplementation() : base(isAvailable: true)
        {
        }

        public override void Prepare(IImageInputSource source, TextRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }

        public override void Process(OnProcessUpdateInternalCallback<TextRecognizerResult> callback)
        {
            if (callback != null)
            {
                callback(new TextRecognizerResult(CreateDummyTextGroup(), null));
            }
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            if (callback != null)
            {
                callback(null);
            }
        }

        private TextGroup CreateDummyTextGroup()
        {
            string languageDetected = "en";
            string text = "Dummy text";
            UnityEngine.Rect rect = new UnityEngine.Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height);
            TextGroup.Element element = new TextGroup.Element(text, languageDetected, rect);
            TextGroup.Line line = new TextGroup.Line(element.ToString(), new List<TextGroup.Element>() { element }, languageDetected, rect);
            TextGroup.Block block = new TextGroup.Block(text, new List<TextGroup.Line>() { line }, languageDetected, rect);
            TextGroup textGroup = new TextGroup(text, new List<TextGroup.Block>() { block });

            return textGroup;
        }
    }
}
#endif