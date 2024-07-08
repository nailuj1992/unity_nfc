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
    public class TextRecognizer
    {
        #region Fields

        private ITextRecognizerImplementation m_implementation;

        #endregion

        #region Properties

        public TextRecognizerOptions Options
        {
            get;
            private set;
        }

        #endregion


        #region Static properties

        public static TextRecognizerUnitySettings UnitySettings
        {
            get
            {
                return EasyMLKitSettings.Instance.TextRecognizerSettings;
            }
        }

        #endregion

        /// <summary>
        /// Create TextRecognizer instance.
        /// </summary>
        public TextRecognizer()
        {
            try
            {
                m_implementation = NativeFeatureActivator.CreateInterface<ITextRecognizerImplementation>(ImplementationBlueprint.TextRecognizer, UnitySettings.IsEnabled);
            }
            catch (Exception e)
            {
                DebugLogger.LogError($"[{Application.platform}] Failed creating required implementation with exception : " + e);
            }

            if(m_implementation == null)
            {
                DebugLogger.LogWarning($"[{Application.platform}] Using null implementation as no platform specific implementation found");
                m_implementation = new Implementations.Null.TextRecognizerImplementation();
            }
        }


        /// <summary>
        /// Pass input source to consider for creating a TextRecognizer instance.
        /// </summary>
        /// <param name="inputSource"></param>
        [Obsolete("Passing Input source in constructor is deprecated. Please pass input source when calling Prepare method.", true)]
        public TextRecognizer(IImageInputSource inputSource)
        {
            try
            {
                m_implementation = NativeFeatureActivator.CreateInterface<ITextRecognizerImplementation>(ImplementationBlueprint.TextRecognizer, UnitySettings.IsEnabled);
            }
            catch (Exception e)
            {
                DebugLogger.LogError($"[{Application.platform}] Failed creating required implementation with exception : " + e);
            }

            if (m_implementation == null)
            {
                DebugLogger.LogWarning($"[{Application.platform}] Using null implementation as no platform specific implementation found");
                m_implementation = new Implementations.Null.TextRecognizerImplementation();
            }
        }


        /// <summary>
        /// Prepare with options. Callback will be called once prepare is complete.
        /// </summary>
        /// <param name="inputSource"></param>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        public void Prepare(IImageInputSource inputSource, TextRecognizerOptions options, OnPrepareCompleteCallback<TextRecognizer> callback)
        {
            Options = options;
            m_implementation.Prepare(inputSource, options, (error) => callback?.Invoke(this, error));
        }

        /// <summary>
        /// Prepare with options. Callback will be called once prepare is complete.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        [Obsolete("This method is obsolete. Pass Input source instance as first parameter to Prepare method.", true)]
        public void Prepare(TextRecognizerOptions options, OnPrepareCompleteCallback<TextRecognizer> callback)
        {
            Options = options;
            m_implementation.Prepare(null, options, (error) => callback?.Invoke(this, error));
        }

        /// <summary>
        /// Start processing the input with the provided options. Callback will be called with TextRecognizerResult once processing has an update/done 
        /// </summary>
        /// <param name="callback"></param>
        public void Process(OnProcessUpdateCallback<TextRecognizer, TextRecognizerResult> callback)
        {
            m_implementation.Process((result) => callback?.Invoke(this, result));
        }

        /// <summary>
        /// Shutdown once the processing is done to release resources
        /// </summary>
        /// <param name="callback"></param>
        public void Close(OnCloseCallback<TextRecognizer> callback)
        {
            m_implementation.Close((error) => callback?.Invoke(this, error));
        }
    }
}

