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
    public class BarcodeScannerDemo : DemoActionPanelBase<BarcodeScannerDemoAction, BarcodeScannerDemoActionType>
    {
        #region Fields

        private bool m_autoClose;

        #endregion

        #region Base class methods

        protected override void OnActionSelectInternal(BarcodeScannerDemoAction selectedAction)
        {
            switch (selectedAction.ActionType)
            {
                case BarcodeScannerDemoActionType.ScanBarcodeFromImage:
                    m_autoClose = true;
                    ScanBarcodeFromImage();
                    break;

                case BarcodeScannerDemoActionType.ScanBarcodeFromLiveCamera:
                    m_autoClose = true;
                    ScanBarcodeFromLiveCamera();
                    break;

                case BarcodeScannerDemoActionType.ScanBarcodeFromARCamera:
                    m_autoClose = false;
                    ScanBarcodeFromARCamera();
                    break;

                case BarcodeScannerDemoActionType.ResourcePage:
                    ProductResources.OpenResourcePage(NativeFeatureType.kBarcodeScanner);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Usecases methods

        private void ScanBarcodeFromImage()
        {
            IImageInputSource inputSource = CreateImageInputSource(DemoResources.GetRandomImage());
            BarcodeScannerOptions options = CreateBarcodeScannerOptions();
            Scan(inputSource, options);
        }

        private void ScanBarcodeFromLiveCamera()
        {
            IImageInputSource inputSource = CreateLiveCameraInputSource();
            BarcodeScannerOptions options = CreateBarcodeScannerOptions();
            Scan(inputSource, options);
        }

        private void ScanBarcodeFromARCamera()
        {
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
            IImageInputSource inputSource = CreateARCameraInputSource();//Now we use live camera as input source
            BarcodeScannerOptions options = CreateBarcodeScannerOptions();
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

        private BarcodeScannerOptions CreateBarcodeScannerOptions()
        {
            BarcodeScannerOptions.Builder builder = new BarcodeScannerOptions.Builder();
            builder.SetScannableFormats(BarcodeFormat.ALL);
            return builder.Build();
        }

        private void Scan(IImageInputSource inputSource, BarcodeScannerOptions options)
        {
            BarcodeScanner scanner = new BarcodeScanner();
            Debug.Log("Starting prepare...");
            scanner.Prepare(inputSource, options, OnPrepareComplete);
        }

        private void OnPrepareComplete(BarcodeScanner scanner, Error error)
        {
            Debug.Log("Prepare complete..." + error);
            if (error == null)
            {
                Log("Prepare completed successfully!");
                scanner.Process(OnProcessUpdate);
            }
            else
            {
                Log("Failed preparing Barcode scanner : " + error.Description);
            }
        }

        private void OnProcessUpdate(BarcodeScanner scanner, BarcodeScannerResult result)
        {
            if (!result.HasError())
            {
                if (result.Barcodes != null)
                {
                    foreach (Barcode each in result.Barcodes)
                    {
                        Log(string.Format("Format : {0}, Value Type : {1}, Raw Value : {2}, Raw Bytes : {3}, Bounding Box : {4}", each.Format, each.ValueType, each.RawValue, each.RawBytes, each.BoundingBox), false);
                    }
                    if (result.Barcodes.Count > 0)
                    {
                        ObjectOverlayController.Instance.ClearAll();
                        foreach (Barcode each in result.Barcodes)
                        {
                            ObjectOverlayController.Instance.ShowOverlay(each.BoundingBox, each.DisplayValue);
                        }

                        if (m_autoClose)
                        {
                            scanner.Close(null);
                        }
                    }
                }
            }
            else
            {
                Log("Barcode scanner failed processing : " + result.Error.Description, false);
            }
        }

        #endregion
    }
}
