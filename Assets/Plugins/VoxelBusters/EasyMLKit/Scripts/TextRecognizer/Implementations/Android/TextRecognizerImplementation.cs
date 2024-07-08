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
    public class TextRecognizerImplementation : NativeImageBasedInputFeatureBase, ITextRecognizerImplementation
    {
        #region Private fields
        
        private bool m_initialised;
        private NativeGoogleMLKitTextRecognizer m_instance;
        private OnPrepareCompleteInternalCallback m_prepareCompleteCallback;
        private OnProcessUpdateInternalCallback<TextRecognizerResult> m_processUpdateCallback;
        private OnCloseInternalCallback m_closeCallback;

        public bool IsAvailable => true;

        public NativeObjectRef NativeObjectRef => throw new NotImplementedException();

        #endregion

        public TextRecognizerImplementation() : base()
        {
            m_instance = new NativeGoogleMLKitTextRecognizer(NativeUnityPluginUtility.GetContext());
        }

        public TextRecognizerImplementation(IImageInputSource inputSource) : base()
        {
            m_instance = new NativeGoogleMLKitTextRecognizer(NativeUnityPluginUtility.GetContext());
        }

        public void Prepare(IImageInputSource inputSource, TextRecognizerOptions options, OnPrepareCompleteInternalCallback callback)
        {
            if (!m_initialised)
            {
                Initialise();
            }

            base.Prepare(inputSource);
            m_prepareCompleteCallback = callback;
            m_instance.Prepare(GetNativeInputImageProducer(inputSource), CreateNativeOptions(options));
        }

        public void Process(OnProcessUpdateInternalCallback<TextRecognizerResult> callback)
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

        private void Initialise()
        {
            SetupListener();
            m_initialised = true;
        }

        
        private void SetupListener()
        {
            m_instance.SetListener(new NativeGoogleMLKitTextRecognizerListener()
            {
                onScanSuccessCallback = (NativeText nativeText, NativeInputImageInfo inputDimensions) =>
                {
                    if (m_processUpdateCallback != null)
                    {
                        TextGroup textGroup = GetTextGroup(nativeText, inputDimensions);
                        Callback callback = () => m_processUpdateCallback(new TextRecognizerResult(textGroup, null));
                        CallbackDispatcher.InvokeOnMainThread(callback);
                    }
                },
                onScanFailedCallback = (NativeException exception) =>
                {
                    if (m_processUpdateCallback != null)
                    {
                        Callback callback = () => m_processUpdateCallback(new TextRecognizerResult(null, new Error(exception.GetMessage())));
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

        private NativeTextRecognizerScanOptions CreateNativeOptions(TextRecognizerOptions options)
        {
            NativeTextRecognizerScanOptions nativeOptions = new NativeTextRecognizerScanOptions(ConvertLanguage(options.InputLanguage));
            return nativeOptions;
        }

        private TextGroup GetTextGroup(NativeText nativeText, NativeInputImageInfo inputImageInfo)
        {

            List<TextGroup.Block> blocks = GetTextBlocks(nativeText.GetTextBlocks().Get(), inputImageInfo);
            TextGroup textGroup = new TextGroup(nativeText.GetText(), blocks);
            return textGroup;
        }


        private List<TextGroup.Block> GetTextBlocks(List<NativeTextBlock> nativeTextBlocks, NativeInputImageInfo inputImageInfo)
        {
            List<TextGroup.Block> blocks = new List<TextGroup.Block>();

            foreach(NativeTextBlock nativeTextBlock in nativeTextBlocks)
            {
                List<TextGroup.Line> lines = GetTextLines(nativeTextBlock.GetLines().Get(), inputImageInfo);
                blocks.Add(new TextGroup.Block(nativeTextBlock.GetText(), lines, nativeTextBlock.GetRecognizedLanguage(), GetRect(nativeTextBlock.GetBoundingBox(), inputImageInfo)));                
            }
            return blocks;
        }


        private List<TextGroup.Line> GetTextLines(List<NativeLine> nativeTextLines, NativeInputImageInfo inputImageInfo)
        {
            List<TextGroup.Line> lines = new List<TextGroup.Line>();

            foreach(NativeLine nativeTextLine in nativeTextLines)
            {
                List<TextGroup.Element> elements = GetTextElements(nativeTextLine.GetElements().Get(), inputImageInfo);
                TextGroup.Line line = new TextGroup.Line(nativeTextLine.GetText(), elements, nativeTextLine.GetRecognizedLanguage(), GetRect(nativeTextLine.GetBoundingBox(), inputImageInfo));
                lines.Add(line);
            }
            return lines;
        }

        private List<TextGroup.Element> GetTextElements(List<NativeElement> nativeElements, NativeInputImageInfo inputImageInfo)
        {
            List<TextGroup.Element> elements = new List<TextGroup.Element>();

            foreach (NativeElement nativeTextElement in nativeElements)
            {
                TextGroup.Element element = new TextGroup.Element(nativeTextElement.GetText(), nativeTextElement.GetRecognizedLanguage(), GetRect(nativeTextElement.GetBoundingBox(), inputImageInfo));
                elements.Add(element);
            }
            return elements;
        }

        private Rect GetRect(NativeRect nativeRect, NativeInputImageInfo imageInfo)
        {
            Rect rawRect = new Rect(nativeRect.Left, nativeRect.Top, nativeRect.Right - nativeRect.Left, nativeRect.Bottom - nativeRect.Top);
            return InputSourceUtility.TransformRectToUserSpace(rawRect, m_inputSource.GetWidth(), m_inputSource.GetHeight(), imageInfo.GetWidth(), imageInfo.GetHeight(), imageInfo.GetRotation());
        }

        private string ConvertLanguage(TextRecognizerInputLanguage inputLanguage)
        {
            switch (inputLanguage)
            {
                case TextRecognizerInputLanguage.Latin:
                    return NativeTextRecognizerInputLanguage.Latin;
                case TextRecognizerInputLanguage.Chinese:
                    return NativeTextRecognizerInputLanguage.Chinese;
                case TextRecognizerInputLanguage.Devanagari:
                    return NativeTextRecognizerInputLanguage.Devanagari;
                case TextRecognizerInputLanguage.Japanese:
                    return NativeTextRecognizerInputLanguage.Japanese;
                case TextRecognizerInputLanguage.Korean:
                    return NativeTextRecognizerInputLanguage.Korean;
                default:
                    throw new Exception("Invalid input language specified!");
            }
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