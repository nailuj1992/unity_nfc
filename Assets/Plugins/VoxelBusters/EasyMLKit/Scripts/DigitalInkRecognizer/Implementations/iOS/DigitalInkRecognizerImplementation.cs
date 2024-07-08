#if UNITY_IOS
using System;
using System.Collections.Generic;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Implementations.iOS.Internal;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit.Implementations.iOS
{
    public partial class DigitalInkRecognizerImplementation : DigitalInkRecognizerImplementationBase
    {
        #region Private fields
        
        private bool m_initialised;
        private NativeDigitalInkRecognizer m_instance;
        private IInputSource m_inputSource;
        private DigitalInkRecognizerModelManager m_modelManager;

        #endregion

        public DigitalInkRecognizerImplementation() : base(isAvailable: true)
        {
            m_instance = new NativeDigitalInkRecognizer();
        }

        public override void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            m_inputSource = inputSource;

            if (!m_initialised)
            {
                Initialise();
            }

            NativeDrawing nativeInput = GetNativeDrawingInputSource(inputSource);
            NativeDigitalInkRecognizerOptions nativeOptions = CreateNativeOptions(options);

            NativeCallbackData onPrepareSuccess = new NativeCallback().Get(() =>
            {
                CallbackDispatcher.InvokeOnMainThread(() => callback(null));
            });

            NativeCallbackData<NativeError> onPrepareFailed = new NativeCallback().Get<NativeError>((error) =>
            {
                CallbackDispatcher.InvokeOnMainThread(() => callback(new Error(error.Description)));
            });


            m_instance.Prepare(ref nativeInput, ref nativeOptions, onPrepareSuccess, onPrepareFailed);
        }

        public override void Process(OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> callback)
        {
            NativeCallbackData<NativeArray> onProcessUpdateSuccess = new NativeCallback().Get<NativeArray>((recognizedValues) =>
            {
                DigitalInkRecognizerResultParser parser = new DigitalInkRecognizerResultParser(recognizedValues);
                List<DigitalInkRecognizedValue> values = parser.GetResult();

                DigitalInkRecognizerResult result = new DigitalInkRecognizerResult(values, null);
                callback(result);
                CallbackDispatcher.InvokeOnMainThread(() => callback(result));
            });

            NativeCallbackData<NativeError> onProcessUpdateFailed = new NativeCallback().Get<NativeError>((error) =>
            {
                CallbackDispatcher.InvokeOnMainThread(() => callback(new DigitalInkRecognizerResult(null, new Error(error.Description))));
            });

            m_instance.Process(onProcessUpdateSuccess, onProcessUpdateFailed);
        }

        public override void Close(OnCloseInternalCallback callback)
        {
            m_instance.Close();

            if(callback != null)
            {
                callback(null);
            }
        }

        private void Initialise()
        {
            m_initialised = true;
        }


        private NativeDigitalInkRecognizerOptions CreateNativeOptions(DigitalInkRecognizerOptions options)
        {
            NativeDigitalInkRecognizerOptions nativeOptions = new NativeDigitalInkRecognizerOptions(options.ModelIdentifier.GetIdentifier(), options.Width, options.Height, options.PreContext);
            return nativeOptions;
        }

        private NativeDrawing GetNativeDrawingInputSource(IDrawingInputSource inputSource)
        {
            DrawingStroke[] strokes = inputSource.GetStrokes();
            NativeDrawing input = new NativeDrawing();
            int strokesLength = strokes.Length;
            input.Count = strokesLength;
            input.Strokes = new NativeDrawingStroke[strokesLength];

            for(int i=0; i < strokesLength; i++)
            {
                DrawingStroke eachStroke = strokes[i];
                DrawingPoint[] points = eachStroke.GetPoints();

                NativeDrawingStroke nativeStroke = new NativeDrawingStroke();
                int pointsLength = points.Length;
                nativeStroke.Count = pointsLength;
                nativeStroke.Points = new NativeDrawingStrokePoint[pointsLength];

                for (int j = 0; j < pointsLength; j++)
                {
                        DrawingPoint eachPoint = points[j];
                        nativeStroke.Points[j] = new NativeDrawingStrokePoint(eachPoint.GetX(), eachPoint.GetY(), eachPoint.GetTimestamp());
                }
                input.Strokes[i] = nativeStroke;
            }

            return input;
        }

        public override DigitalInkRecognizerModelManager GetModelManager()
        {
            if (m_modelManager == null)
            {
                NativeDigitalInkRecognizerModelManager nativeManager = m_instance.GetModelManager();
                m_modelManager = new ModelManager(nativeManager);
            }

            return m_modelManager;
        }

    }
}
#endif