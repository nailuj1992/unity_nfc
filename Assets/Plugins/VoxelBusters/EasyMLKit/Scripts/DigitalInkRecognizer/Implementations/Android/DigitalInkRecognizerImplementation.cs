#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.CoreLibrary.NativePlugins.Android;
using VoxelBusters.EasyMLKit.Implementations.Android.Internal;
using VoxelBusters.EasyMLKit.Internal;
using VoxelBusters.EasyMLKit.NativePlugins.Android;

namespace VoxelBusters.EasyMLKit.Implementations.Android
{
    public partial class DigitalInkRecognizerImplementation : IDigitalInkRecognizerImplementation
    {
        #region Private fields
        
        private bool m_initialised;
        private NativeDigitalInkRecognizer m_instance;
        private OnPrepareCompleteInternalCallback m_prepareCompleteCallback;
        private OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> m_processUpdateCallback;
        private OnCloseInternalCallback m_closeCallback;
        private DigitalInkRecognizerModelManager m_modelManager;

        public bool IsAvailable => true;

        public NativeObjectRef NativeObjectRef => throw new NotImplementedException();

        #endregion

        public DigitalInkRecognizerImplementation() : base()
        {
            m_instance = new NativeDigitalInkRecognizer(NativeUnityPluginUtility.GetContext());
        }

        public void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (!m_initialised)
            {
                Initialise();
            }

            m_prepareCompleteCallback = callback;
            m_instance.Prepare(GetNativeDrawingInputSource(inputSource), CreateNativeOptions(options));
        }

        public void Process(OnProcessUpdateInternalCallback<DigitalInkRecognizerResult> callback)
        {
            m_processUpdateCallback = callback;
            m_instance.Process();
        }

        public void Close(OnCloseInternalCallback callback)
        {
            m_closeCallback = callback;
            m_instance.Close();

            if(m_closeCallback != null)
            {
                m_closeCallback(null);
            }
        }

        public DigitalInkRecognizerModelManager GetModelManager()
        {
            if(m_modelManager == null)
            {
                NativeDigitalInkRecognizerModelManager nativeManager = m_instance.GetModelManager();
                m_modelManager = new ModelManager(nativeManager);
            }

            return m_modelManager;
        }

        private void Initialise()
        {
            SetupListener();
            m_initialised = true;
        }

        
        private void SetupListener()
        {
            m_instance.SetListener(new NativeDigitalInkRecognizerListener()
            {
                onScanSuccessCallback = (NativeList<NativeDigitalInkRecognizedValue> nativeList) =>
                {
                    if (m_processUpdateCallback != null)
                    {
                        List<DigitalInkRecognizedValue> values = GetValues(nativeList);
                        Callback callback = () => m_processUpdateCallback(new DigitalInkRecognizerResult(values, null));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onScanFailedCallback = (NativeException exception) =>
                {
                    if (m_processUpdateCallback != null)
                    {
                        Callback callback = () => m_processUpdateCallback(new DigitalInkRecognizerResult(null, new Error(exception.GetMessage())));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onPrepareSuccessCallback = () =>
                {
                    if (m_prepareCompleteCallback != null)
                    {
                        Callback callback = () => m_prepareCompleteCallback(null);
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onPrepareFailedCallback = (NativeException exception) =>
                {
                    if (m_prepareCompleteCallback != null)
                    {
                        Callback callback = () => m_prepareCompleteCallback(new Error(exception.GetMessage()));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                }
            });
        }

        private List<DigitalInkRecognizedValue> GetValues(NativeList<NativeDigitalInkRecognizedValue> nativeList)
        {
            List<NativeDigitalInkRecognizedValue> recognizedValues = nativeList.Get();
            List<DigitalInkRecognizedValue> values = new List<DigitalInkRecognizedValue>();

            foreach(NativeDigitalInkRecognizedValue nativeValue in recognizedValues)
            {
                float? score = nativeValue.GetScore();

                if(!nativeValue.HasScore())
                {
                    score = null;
                }

                values.Add(new DigitalInkRecognizedValue(nativeValue.GetText(), score));
            }

            return values;
        }

        private NativeDigitalInkRecognizerInput GetNativeDrawingInputSource(IDrawingInputSource inputSource)
        {
            NativeDigitalInkRecognizerInput input = new NativeDigitalInkRecognizerInput();
            DrawingStroke[] strokes = inputSource.GetStrokes();

            foreach(DrawingStroke eachStroke in strokes)
            {
                
                DrawingPoint[] points = eachStroke.GetPoints();
                NativeDigitalInkStroke nativeStroke = new NativeDigitalInkStroke();

                foreach (DrawingPoint eachPoint in points)
                {
                    nativeStroke.AddPoint(new NativeDigitalInkPoint(eachPoint.GetX(), eachPoint.GetY(), eachPoint.GetTimestamp()));
                }

                input.AddStroke(nativeStroke);
            }

            return input;
        }


        private NativeDigitalInkRecognizerScanOptions CreateNativeOptions(DigitalInkRecognizerOptions options)
        {
            NativeDigitalInkRecognizerModelIdentifier identifier = new NativeDigitalInkRecognizerModelIdentifier(options.ModelIdentifier.GetIdentifier());
            NativeDigitalInkRecognizerScanOptions nativeOptions = new NativeDigitalInkRecognizerScanOptions(identifier, options.Width, options.Height, options.PreContext);
            return nativeOptions;
        }

        public IntPtr AddrOfNativeObject()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
#endif