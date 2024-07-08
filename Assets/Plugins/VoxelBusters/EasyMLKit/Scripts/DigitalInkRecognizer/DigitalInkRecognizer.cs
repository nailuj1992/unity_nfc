using System;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Scan and recognize text by passing different input sources.
    /// </summary>
    public class DigitalInkRecognizer
    {
        #region Fields

        private IDigitalInkRecognizerImplementation m_implementation;

        #endregion

        #region Properties

        public DigitalInkRecognizerOptions Options
        {
            get;
            private set;
        }

        #endregion


        #region Static properties

        public static DigitalInkRecognizerUnitySettings UnitySettings
        {
            get
            {
                return EasyMLKitSettings.Instance.DigitalInkRecognizerSettings;
            }
        }

        #endregion

        /// <summary>
        /// Pass input source to consider for creating a DigitalInkRecognizer instance.
        /// </summary>
        /// <param name="inputSource"></param>
        public DigitalInkRecognizer()
        {
            try
            {
                m_implementation = NativeFeatureActivator.CreateInterface<IDigitalInkRecognizerImplementation>(ImplementationBlueprint.DigitalInkRecognizer, UnitySettings.IsEnabled);
            }
            catch (Exception e)
            {
                DebugLogger.LogError($"[{Application.platform}] Failed creating required implementation with exception : " + e);
            }

            if(m_implementation == null)
            {
                DebugLogger.LogWarning($"[{Application.platform}] Using null implementation as no platform specific implementation found");
                m_implementation = new Implementations.Null.DigitalInkRecognizerImplementation();
            }
        }

        public DigitalInkRecognizerModelManager GetModelManager()
        {
            return m_implementation.GetModelManager();
        }

        /// <summary>
        /// Prepare with options. Callback will be called once prepare is complete.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        public void Prepare(IDrawingInputSource inputSource, DigitalInkRecognizerOptions options, OnPrepareCompleteCallback<DigitalInkRecognizer> callback)
        {
            Options = options;
            m_implementation.Prepare(inputSource, options, (error) => callback?.Invoke(this, error));
        }

        /// <summary>
        /// Start processing the input with the provided options. Callback will be called with DigitalInkRecognizerResult once processing has an update/done 
        /// </summary>
        /// <param name="callback"></param>
        public void Process(OnProcessUpdateCallback<DigitalInkRecognizer, DigitalInkRecognizerResult> callback)
        {
            m_implementation.Process((result) => callback?.Invoke(this, result));
        }

        /// <summary>
        /// Shutdown once the processing is done to release resources
        /// </summary>
        /// <param name="callback"></param>
        public void Close(OnCloseCallback<DigitalInkRecognizer> callback)
        {
            m_implementation.Close((error) => callback?.Invoke(this, error));
        }
    }
}

