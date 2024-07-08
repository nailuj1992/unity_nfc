using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EasyMLKit;

namespace DigitsNFCToolkit.Samples
{
    public class PassportScan
    {
        private const int NumberOfAttempts = 100;

        private TaskCompletionSource<string> _tcs;

        private const string TextExample = @"
P<UTOQUEST<AMELIA<MARY<
s012345674UTO8704234F3208265<<<2
            ";

        public async Task<string> Recognize(CancellationToken cancellationToken, ITextureGetter textureGetter = null)
        {
            int attemptsLeft = NumberOfAttempts;

            try
            {
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    _tcs = new TaskCompletionSource<string>();

                    ScanText(textureGetter);

                    await _tcs.Task;
                    if (!string.IsNullOrEmpty(_tcs.Task.Result))
                    {
                        break;
                    }
                    attemptsLeft--;
                    Log("attemptsLeft = " + attemptsLeft);
                    if (attemptsLeft == 0)
                    {
                        break;
                    }

                    await Task.Delay(1000, cancellationToken);
                }
            }
            catch (Exception e)
            {
                Log(e.ToString());
                //throw;
            }

            return _tcs.Task.IsCompletedSuccessfully ? _tcs.Task.Result : null;
        }


        private void ScanText(ITextureGetter textureGetter = null)
        {
#if UNITY_EDITOR
            SetResult();
#elif UNITY_IOS || UNITY_ANDROID
            if (textureGetter != null)
            {
                ScanTextFromImage(textureGetter.GetCameraTexture());
            }
            else
            {
                ScanTextFromLiveCamera();
            }
#endif
        }

        private void ScanTextFromImage(Texture2D texture)
        {
            IImageInputSource inputSource = new ImageInputSource(texture);
            TextRecognizerOptions options = CreateTextRecognizerOptions();
            Scan(inputSource, options);
        }

        private void ScanTextFromLiveCamera()
        {
            IImageInputSource inputSource = new LiveCameraInputSource()
            {
                EnableFlash = false,
                IsFrontFacing = false
            };
            TextRecognizerOptions options = CreateTextRecognizerOptions();
            Scan(inputSource, options);
        }

        private TextRecognizerOptions CreateTextRecognizerOptions()
        {
            TextRecognizerOptions.Builder builder = new TextRecognizerOptions.Builder();
            builder.SetInputLanguage(TextRecognizerInputLanguage.Latin);
            return builder.Build();
        }

        private void Scan(IImageInputSource inputSource, TextRecognizerOptions options)
        {
            TextRecognizer scanner = new TextRecognizer();
            Log("Starting prepare...");
            scanner.Prepare(inputSource, options, OnPrepareComplete);
        }

        private void OnPrepareComplete(TextRecognizer scanner, Error error)
        {
            Log("Prepare complete..." + error);
            if (error == null)
            {
                Log("Prepare completed successfully!");
                scanner.Process(OnProcessUpdate);
            }
            else
            {
                Log("Failed preparing Text Recognizer : " + error.Description);
            }
        }

        private void OnProcessUpdate(TextRecognizer scanner, TextRecognizerResult result)
        {
            if (!result.HasError())
            {
                TextGroup textGroup = result.TextGroup;
                if (textGroup != null && !string.IsNullOrEmpty(textGroup.Text))
                {
                    SetResult(textGroup.Text);
                }
                else
                {
                    SetResult(null);
                }
            }
            else
            {
                SetResult(null);
                Log("Text Recognizer failed processing : " + result.Error.Description);
            }
            scanner.Close(null);
        }

        private async void SetResult()
        {
            await Task.Delay(1);
            SetResult(TextExample);
        }

        private void SetResult(string result)
        {
            //Log(result);
            if (string.IsNullOrEmpty(result))
            {
                _tcs.SetResult(result);
                return;
            }
            string mrz = MRZHelper.ExtractMRZ(result);
            //Log(mrz);
            _tcs.SetResult(mrz);
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}
