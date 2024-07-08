using System;
using UnityEngine;
using UnityEngine.UI;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;
using VoxelBusters.EasyMLKit.Internal;
using System.Collections.Generic;
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

// internal namespace
namespace VoxelBusters.EasyMLKit.Demo
{
    public class DigitalInkRecognizerDemo : DemoActionPanelBase<DigitalInkRecognizerDemoAction, DigitalInkRecognizerDemoActionType>, IDrawingStrokeListener
    {
        #region Fields
        [SerializeField]
        private DrawingAreaHandler m_drawingAreaHandler;
        private IDrawingInputSource m_inputSource;
        private List<DrawingStroke> m_drawingStrokes = new List<DrawingStroke>();

        private readonly DigitalInkRecognizerModelIdentifier modelIdentifier = DigitalInkRecognizerModelIdentifier.English;
        private DigitalInkRecognizer m_recognizer;
        #endregion


        #region Base class methods

        protected override void Awake()
        {
            base.Awake();
            m_drawingAreaHandler.SetStorkeListener(this);
            m_recognizer = new DigitalInkRecognizer();
        }

        protected override void OnActionSelectInternal(DigitalInkRecognizerDemoAction selectedAction)
        {
            switch (selectedAction.ActionType)
            {
                case DigitalInkRecognizerDemoActionType.CheckModelAvailability:
                    IsModelAvailable();
                    break;

                case DigitalInkRecognizerDemoActionType.DownloadModel:
                    DownloadModel();
                    break;

                case DigitalInkRecognizerDemoActionType.DeleteModel:
                    DeleteModel();
                    break;

                case DigitalInkRecognizerDemoActionType.ScanDrawingFromCanvas:
                    ScanDrawingFromCanvas();
                    break;

                case DigitalInkRecognizerDemoActionType.ResourcePage:
                    ProductResources.OpenResourcePage(NativeFeatureType.kDigitalInkRecognizer);
                    break;

                default:
                    break;
            }
        }

        private void DeleteModel()
        {
            DigitalInkRecognizerModelManager modelManager = GetModelManager();
            modelManager.DeleteModel(modelIdentifier, (Error error) =>
            {
                if(error == null)
                {
                    Log("Successfully deleted model");
                }
                else
                {
                    Log("Failed deleting model : " + error);
                }
            });
        }

        private void DownloadModel()
        {
            DigitalInkRecognizerModelManager modelManager = GetModelManager();
            modelManager.DownloadModel(modelIdentifier, (Error error) =>
            {
                if (error == null)
                {
                    Log("Successfully downloaded model");
                }
                else
                {
                    Log("Failed downloading model model : " + error);
                }
            });
        }

        private bool IsModelAvailable()
        {
            DigitalInkRecognizerModelManager modelManager = GetModelManager();
            bool isAvailable = modelManager.IsModelAvailable(modelIdentifier);

            Log("Model available : " + isAvailable);

            return isAvailable;
        }

        private DigitalInkRecognizerModelManager GetModelManager()
        {
            DigitalInkRecognizerModelManager modelManager = m_recognizer.GetModelManager();
            return modelManager;
        }

        #endregion

        #region Usecases methods

        private void ScanDrawingFromCanvas()
        {            
            IDrawingInputSource inputSource = GetInputSource();
            DigitalInkRecognizerOptions options = CreateDigitalInkRecognizerOptions();
            Prepare(inputSource, options);
        }

        #endregion

        #region Utility methods

        private IDrawingInputSource GetInputSource()
        {
            m_inputSource = new DrawingInputSource(m_drawingAreaHandler.GetAreaWidth(), m_drawingAreaHandler.GetAreaHeight());
            foreach(DrawingStroke each in m_drawingStrokes)
            {
                m_inputSource.AddStroke(each);
            }

            return m_inputSource;
        }


        private DigitalInkRecognizerOptions CreateDigitalInkRecognizerOptions()
        {
            DigitalInkRecognizerOptions.Builder builder = new DigitalInkRecognizerOptions.Builder();
            builder.SetModelIdentifier(modelIdentifier);
            builder.SetWidth(m_inputSource.GetWidth());
            builder.SetHeight(m_inputSource.GetHeight());
            builder.SetPreContext(null);

            return builder.Build();
        }

        private void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options)
        {
            Debug.Log("Starting prepare...");
            m_recognizer.Prepare(inputSource, options, OnPrepareComplete);
        }

        private void OnPrepareComplete(DigitalInkRecognizer scanner, Error error)
        {
            Debug.Log("Prepare complete..." + error);
            if (error == null)
            {
                Log("Prepare completed successfully!");
                scanner.Process(OnProcessUpdate);
            }
            else
            {
                Log("Failed preparing Digital Ink Recognizer : " + error.Description);
            }
        }

        private void OnProcessUpdate(DigitalInkRecognizer scanner, DigitalInkRecognizerResult result)
        {
            if (!result.HasError())
            {
                Log(string.Format("Recognized values : {0}", result.Values.Count), false);

                foreach (DigitalInkRecognizedValue each in result.Values)
                {
                    Log(string.Format("Recognized Text : {0}", each.Text));
                }

                Cleanup(scanner);
            }
            else
            {
                Log("Digital Ink Recognizer failed processing : " + result.Error.Description);
            }
        }

        private void Cleanup(DigitalInkRecognizer scanner)
        {
            scanner.Close(null);
            m_drawingStrokes.Clear();
            m_drawingAreaHandler.ClearDrawing();
        }

        public void OnNewStroke(DrawingStroke stroke)
        {
            Debug.Log("On new stroke");
            m_drawingStrokes.Add(stroke);
        }

        #endregion
    }
}
