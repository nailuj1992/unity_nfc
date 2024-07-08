using System;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;
using VoxelBusters.EasyMLKit.Internal;
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

// internal namespace
namespace VoxelBusters.EasyMLKit.Demo
{
    public class TextRecognizerDemo : DemoActionPanelBase<TextRecognizerDemoAction, TextRecognizerDemoActionType>
    {
        #region Fields
        private bool m_autoClose;
        #endregion


        #region Base class methods

        protected override void OnActionSelectInternal(TextRecognizerDemoAction selectedAction)
        {
            switch (selectedAction.ActionType)
            {
                case TextRecognizerDemoActionType.ScanTextFromImage:
                    m_autoClose = true;
                    ScanTextFromImage();
                    break;

                case TextRecognizerDemoActionType.ScanTextFromLiveCamera:
                    m_autoClose = true;
                    ScanTextFromLiveCamera();
                    break;

                case TextRecognizerDemoActionType.ScanTextFromARCamera:
                    m_autoClose = false;
                    ScanTextFromARCamera();
                    break;

                case TextRecognizerDemoActionType.ResourcePage:
                    ProductResources.OpenResourcePage(NativeFeatureType.kTextRecognizer);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Usecases methods

        private void ScanTextFromImage()
        {
            IImageInputSource inputSource = CreateImageInputSource(DemoResources.GetRandomImage());
            TextRecognizerOptions options = CreateTextRecognizerOptions();
            Scan(inputSource, options);
        }

        private void ScanTextFromLiveCamera()
        {
            IImageInputSource inputSource = CreateLiveCameraInputSource();
            TextRecognizerOptions options = CreateTextRecognizerOptions();
            Scan(inputSource, options);
        }

        private void ScanTextFromARCamera()
        {
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
            IImageInputSource inputSource = CreateARCameraInputSource();//Now we use live camera as input source
            TextRecognizerOptions options = CreateTextRecognizerOptions();
            Scan(inputSource, options);
#else
            Log("AR Foundation support not enabled. Add EASY_ML_KIT_SUPPORT_AR_FOUNDATION scripting define if you want to use AR Foundation camera");
#endif
        }

        #endregion

        #region Utility methods

        private IImageInputSource CreateImageInputSource(Texture2D texture)
        {
            return new ImageInputSource(texture);
        }

        private IImageInputSource CreateLiveCameraInputSource()
        {
            LiveCameraInputSource inputSource = new LiveCameraInputSource();
            inputSource.EnableFlash = false;
            inputSource.IsFrontFacing = false;

            return inputSource;
        }

#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
        private IImageInputSource CreateARCameraInputSource()
        {
            ARSession arSession = FindObjectOfType<ARSession>();
            ARCameraManager arCameraManager = FindObjectOfType<ARCameraManager>();
            IImageInputSource inputSource = new ARFoundationCameraInputSource(arSession, arCameraManager);
            return inputSource;
        }
#endif

        private TextRecognizerOptions CreateTextRecognizerOptions()
        {
            TextRecognizerOptions.Builder builder = new TextRecognizerOptions.Builder();
            builder.SetInputLanguage(TextRecognizerInputLanguage.Latin);
            return builder.Build();
        }

        private void Scan(IImageInputSource inputSource, TextRecognizerOptions options)
        {
            TextRecognizer scanner = new TextRecognizer();
            Debug.Log("Starting prepare...");
            scanner.Prepare(inputSource, options, OnPrepareComplete);
        }

        private void OnPrepareComplete(TextRecognizer scanner, Error error)
        {
            Debug.Log("Prepare complete..." + error);
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
                Log(string.Format("Text : {0}", result.TextGroup.Text), false);
                TextGroup textGroup = result.TextGroup;

                if (textGroup != null)
                {
                    ObjectOverlayController.Instance.ClearAll();
                    if (textGroup.Blocks != null)
                    {
                        foreach (TextGroup.Block each in textGroup.Blocks)
                        {
                            ObjectOverlayController.Instance.ShowOverlay(each.BoundingBox, string.Format("{0}", each.Text));
                        }
                    }

                    if(m_autoClose)
                    {
                        scanner.Close(null);
                    }
                }
            }
            else
            {
                Log("Text Recognizer failed processing : " + result.Error.Description, false);
            }
        }

        #endregion
    }
}
