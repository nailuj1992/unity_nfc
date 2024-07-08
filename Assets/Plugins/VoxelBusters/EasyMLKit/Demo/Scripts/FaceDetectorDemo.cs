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
    internal class FaceDetectorDemo : DemoActionPanelBase<FaceDetectorDemoAction, FaceDetectorDemoActionType>
    {
        /* #region Fields

         private bool m_autoClose;

         [SerializeField]
         private FaceContoursOverlay m_faceContourOverlay;

         [SerializeField]
         private RawImage m_webCamTextureRawImage;

         private WebCamTexture m_webCamTexture;

         #endregion

         #region Base class methods

         protected override void OnActionSelectInternal(FaceDetectorDemoAction selectedAction)
         {
             switch (selectedAction.ActionType)
             {
                 case FaceDetectorDemoActionType.ScanFromImage:
                     m_autoClose = true;
                     ScanFromImage();
                     break;

                 case FaceDetectorDemoActionType.ScanFromLiveCamera:
                     m_autoClose = true;
                     ScanFromLiveCamera();
                     break;

                 case FaceDetectorDemoActionType.ScanFromARCamera:
                     m_autoClose = false;
                     ScanFromARCamera();
                     break;

                 case FaceDetectorDemoActionType.ResourcePage:
                     ProductResources.OpenResourcePage(NativeFeatureType.kFaceDetector);
                     break;

                 default:
                     break;
             }
         }

         #endregion


         #region Usecases methods

         private void ScanFromImage()
         {
             IImageInputSource inputSource = CreateImageInputSource(DemoResources.GetRandomImage());
             FaceDetectorOptions options = CreateFaceDetectorOptions();
             Scan(inputSource, options);
         }

         private void ScanFromLiveCamera()
         {
             IImageInputSource inputSource = CreateLiveCameraInputSource();
             FaceDetectorOptions options = CreateFaceDetectorOptions();
             Scan(inputSource, options);
         }

         private void ScanFromARCamera()
         {
 #if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
             IImageInputSource inputSource = CreateARCameraInputSource();//Now we use live camera as input source
             FaceDetectorOptions options = CreateFaceDetectorOptions();
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

         private FaceDetectorOptions CreateFaceDetectorOptions()
         {
             FaceDetectorOptions.Builder builder = new FaceDetectorOptions.Builder();
             builder.EnableTracking(true);
             builder.SetClassificationMode(FaceDetectorOptions.ClassificationMode.None);
             builder.SetContourMode(FaceDetectorOptions.ContourMode.All);
             builder.SetLandmarkMode(FaceDetectorOptions.LandmarkMode.All);
             builder.SetPerformanceMode(FaceDetectorOptions.PerformanceMode.Fast);
             return builder.Build();
         }

         private void Scan(IImageInputSource inputSource, FaceDetectorOptions options)
         {
             FaceDetector scanner = new FaceDetector(inputSource);
             Debug.Log("Starting prepare...");
             scanner.Prepare(options, OnPrepareComplete);
         }

         private void OnPrepareComplete(FaceDetector scanner, Error error)
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

         private void OnProcessUpdate(FaceDetector scanner, FaceDetectorResult result)
         {
             if (!result.HasError())
             {
                 foreach (Face each in result.Faces)
                 {
                     Log(string.Format("Tracking Id : {0}, Bounding Box : {1}", each.TrackingId, each.BoundingBox), false);
                 }
                 if (result.Faces.Count > 0)
                 {
                     List<Face> faces = result.Faces;
                     ObjectOverlayController.Instance.ClearAll();
                     foreach (Face each in faces)
                     {
                         ObjectOverlayController.Instance.ShowOverlay(each.BoundingBox, string.Format("Tracking Id : {0}", each.TrackingId));
                     }

                     m_faceContourOverlay.SetFaceContours(faces[0].Contours);

                     if (m_autoClose)
                     {
                         scanner.Close(null);
                     }
                 }
             }
             else
             {
                 Log("Barcode scanner failed processing : " + result.Error.Description, false);
             }
         }

         #endregion*/
    }
}
