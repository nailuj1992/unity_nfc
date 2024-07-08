namespace VoxelBusters.EasyMLKit
{
    public class DigitalInkRecognizedValue
    {
        public string Text { get; }
        private float? Score { get; }

        public DigitalInkRecognizedValue(string text, float? score)
        {
            Text = text;
            Score = score;
        }
    }
}