using System;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins.DemoKit;
using VoxelBusters.EasyMLKit.Internal;
using System.IO;
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

// internal namespace
namespace VoxelBusters.EasyMLKit.Demo
{
    public class ObjectDetectorAndTrackerDemo : DemoActionPanelBase<ObjectDetectorAndTrackerDemoAction, ObjectDetectorAndTrackerDemoType>
    {
        private bool m_autoClose;
        private bool m_detectionInProgress;

        #region Base class methods

        protected override void OnActionSelectInternal(ObjectDetectorAndTrackerDemoAction selectedAction)
        {
            switch (selectedAction.ActionType)
            {
                case ObjectDetectorAndTrackerDemoType.DetectAndTrackFromImage:
                    m_autoClose = true;
                    DetectObjectsFromImage();
                    break;

                case ObjectDetectorAndTrackerDemoType.DetectAndTrackFromLiveCamera:
                    m_autoClose = true;
                    DetectObjectsFromLiveCamera();
                    break;

                case ObjectDetectorAndTrackerDemoType.DetectAndTrackFromARFoundationCamera:
                    m_autoClose = false;
                    DetectObjectsFromARCamera();
                    break;
                case ObjectDetectorAndTrackerDemoType.ResourcePage:
                    ProductResources.OpenResourcePage(NativeFeatureType.kObjectDetectorAndTracker);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Usecases methods

        private void DetectObjectsFromImage()
        {
            IImageInputSource inputSource = CreateImageInputSource(DemoResources.GetRandomImage());
            ObjectDetectorAndTrackerOptions options = CreateObjectDetectorAndTrackerOptions();
            DetectAndTrack(inputSource, options);
        }

        private void DetectObjectsFromLiveCamera()
        {
            IImageInputSource inputSource = CreateLiveCameraInputSource();
            ObjectDetectorAndTrackerOptions options = CreateObjectDetectorAndTrackerOptions();
            DetectAndTrack(inputSource, options);
        }

        private void DetectObjectsFromARCamera()
        {
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
            IImageInputSource inputSource = CreateARCameraInputSource();//Now we use live camera as input source
            ObjectDetectorAndTrackerOptions options = CreateObjectDetectorAndTrackerOptions();
            DetectAndTrack(inputSource, options);
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

        private ObjectDetectorAndTrackerOptions CreateObjectDetectorAndTrackerOptions()
        {
            ObjectDetectorAndTrackerOptions.Builder builder = new ObjectDetectorAndTrackerOptions.Builder();
            builder.EnableClassification(true);
            builder.EnableMultipleObjectDetection(false);
            //builder.SetCustomModelPath(URLString.FileURLWithPath(Path.Combine(Application.streamingAssetsPath, "food.tflite")));//Place your custom model in top level if using StreamingAssets folder.
            return builder.Build();
        }

        private void DetectAndTrack(IImageInputSource inputSource, ObjectDetectorAndTrackerOptions options)
        {
            ObjectDetectorAndTracker detectorAndTracker = new ObjectDetectorAndTracker();
            Debug.Log("Starting prepare...");
            detectorAndTracker.Prepare(inputSource, options, OnPrepareComplete);
        }

        private void OnPrepareComplete(ObjectDetectorAndTracker detector, Error error)
        {
            if (m_detectionInProgress)
                return;

            if(!m_detectionInProgress)
            {
                m_detectionInProgress = true;
            }

            Debug.Log("Prepare complete..." + error);
            if (error == null)
            {
                Log("Prepare completed successfully!");
                detector.Process(OnProcessUpdate);
            }
            else
            {
                Log("Failed preparing Barcode scanner : " + error.Description);
            }
        }

        private void OnProcessUpdate(ObjectDetectorAndTracker detector, ObjectDetectorAndTrackerResult result)
        {
            if (!result.HasError())
            {
                Log(string.Format("Received {0} detected objects", result.DetectedObjects.Count), false);

                foreach (DetectedObject each in result.DetectedObjects)
                {
                    Log(each.ToString());
                }

                ObjectOverlayController.Instance.ClearAll();

                if (result.DetectedObjects.Count > 0)
                {
                    foreach (DetectedObject each in result.DetectedObjects)
                    {
                        ObjectOverlayController.Instance.ShowOverlay(each.BoundingBox, each.Labels.Count > 0 ? each.Labels[0].Text : "No label");
                    }

                    if (m_autoClose)
                    {
                       detector.Close(OnClose);
                    }
                }
            }
            else
            {
                Log("Object Detector and Tracker failed processing : " + result.Error.Description);
            }
        }

        private void OnClose(ObjectDetectorAndTracker tracker, Error error)
        {
            m_detectionInProgress = false;
        }

        #endregion
    }
}
