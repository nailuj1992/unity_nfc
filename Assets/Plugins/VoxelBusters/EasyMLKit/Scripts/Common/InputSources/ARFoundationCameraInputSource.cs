#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace VoxelBusters.EasyMLKit
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeArStruct
    {
        public int version;
        public IntPtr data;
    }

    /// <summary>
    /// This allows to use AR Foundation's ar camera as input source
    /// </summary>
    public class ARFoundationCameraInputSource : IImageInputSource
    {
        public ARCameraManager CameraManager
        {
            get;
            private set;
        }

        public ARSession Session
        {
            get;
            private set;
        }

        private XRCameraParams m_defaultCameraParams;

        private long m_previousFrameTimestamp = 0;

        public event Action CameraFrameReceived;

        /// <summary>
        /// Pass ARSession and ARCameraManager for creating an instance
        /// </summary>
        /// <param name="session"></param>
        /// <param name="cameraManager"></param>
        public ARFoundationCameraInputSource(ARSession session, ARCameraManager cameraManager)
        {
            Session                     = session;
            CameraManager               = cameraManager;

            m_defaultCameraParams = new XRCameraParams
            {
                zNear = CameraManager.GetComponent<Camera>().nearClipPlane,
                zFar = CameraManager.GetComponent<Camera>().farClipPlane,
                screenWidth = Screen.width,
                screenHeight = Screen.height,
                screenOrientation = Screen.orientation
            };
        }

        public void Register()
        {
            CameraManager.frameReceived += OnCameraFrameReceived;
        }

        public void Unregister()
        {
            CameraManager.frameReceived -= OnCameraFrameReceived;
        }


        private void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
        {
            if(IsNewCameraFrame(eventArgs.timestampNs))
            {
                CameraFrameReceived();
            }
        }

        private bool IsNewCameraFrame(long? timestamp)
        {
            if(timestamp != null)
            {
                return m_previousFrameTimestamp < timestamp;
            }

            return true;
        }


        public XRCpuImage GetLatestCpuImage()
        {
            CameraManager.TryAcquireLatestCpuImage(out XRCpuImage cpuImage);
            return cpuImage;
        }


        public XRCameraFrame GetLatestFrame()
        {
            XRCameraFrame cameraFrame;
            CameraManager.subsystem.TryGetLatestFrame(GetCameraParams(), out cameraFrame);
            return cameraFrame;
        }

        public void Close()
        {
            Unregister();
        }

        public float GetWidth()
        {
            return Screen.width;
        }

        public float GetHeight()
        {
            return Screen.height;
        }

        private XRCameraParams GetCameraParams()
        {
            XRCameraParams cameraParams = m_defaultCameraParams;
            cameraParams.screenWidth = Screen.width;
            cameraParams.screenHeight = Screen.height;
            cameraParams.screenOrientation = Screen.orientation;
            return cameraParams;
        }
    }
#if UNITY_ANDROID
    // 2d coordinate systems supported by ARCore.
    internal enum ApiCoordinates2dType
    {
        TexturePixels = 0,
        TextureNormalized = 1,
        ImagePixels = 2,
        ImageNormalized = 3,
        FeatureTrackingImage = 4,
        FeatureTrackingImageNormalized = 5,
        OpenGLDeviceNormalized = 6,
        View = 7,
        ViewNormalized = 8,
    }


    internal class ExternalApi
    {
        [DllImport("arcore_sdk_c")]
        public static extern int ArFrame_acquireCameraImage(
            IntPtr sessionHandle, IntPtr frameHandle, ref IntPtr imageHandle);

        [DllImport("arcore_sdk_c")]
        public static extern void ArImage_release(IntPtr imageHandle);

        [DllImport("arcore_sdk_c")]
        public static extern void ArFrame_transformCoordinates2d(IntPtr sessionHandle, IntPtr frameHandle,
                ApiCoordinates2dType inputType, int numVertices, ref Vector2 uvsIn,
                ApiCoordinates2dType outputType, ref Vector2 uvsOut);
    }
#endif
}
#endif