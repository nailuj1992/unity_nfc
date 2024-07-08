using System;
using VoxelBusters.CoreLibrary;
using VoxelBusters.CoreLibrary.NativePlugins;
using VoxelBusters.EasyMLKit.Internal;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Feature to detect an object and track it
    /// </summary>
    public class ObjectDetectorAndTracker
    {
        public ObjectDetectorAndTrackerOptions Options
        {
            get;
            private set;
        }

        #region Static properties

        public static ObjectDetectorAndTrackerUnitySettings UnitySettings
        {
            get
            {
                return EasyMLKitSettings.Instance.ObjectDetectorAndTrackerSettings;
            }
        }

        #endregion

        private IObjectDetectorAndTrackerImplementation m_implementation;

        /// <summary>
        /// Create ObjectDetectorAndTracker instance.
        /// </summary>
        public ObjectDetectorAndTracker()
        {
            try
            {
                m_implementation = NativeFeatureActivator.CreateInterface<IObjectDetectorAndTrackerImplementation>(ImplementationBlueprint.ObjectDetectorAndTracker, UnitySettings.IsEnabled);
            }
            catch (Exception e)
            {
                DebugLogger.LogError("Failed creating required implementation with exception : " + e);
                m_implementation = new Implementations.Null.ObjectDetectorAndTrackerImplementation();
            }
        }

        /// <summary>
        /// Pass an input source to create an instance
        /// </summary>
        /// <param name="inputSource"></param>
        [Obsolete("Passing Input source in constructor is deprecated. Pass input source when calling Prepare method.", true)]
        public ObjectDetectorAndTracker(IImageInputSource inputSource)
        {
            try
            {
                m_implementation = NativeFeatureActivator.CreateInterface<IObjectDetectorAndTrackerImplementation>(ImplementationBlueprint.ObjectDetectorAndTracker, UnitySettings.IsEnabled);
            }
            catch (Exception e)
            {
                DebugLogger.LogError("Failed creating required implementation with exception : " + e);
                m_implementation = new Implementations.Null.ObjectDetectorAndTrackerImplementation();
            }
        }

        /// <summary>
        /// Prepare with options and callback gets called once prepare is completed.
        /// </summary>
        /// <param name="inputSource"></param>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        public void Prepare(IImageInputSource inputSource, ObjectDetectorAndTrackerOptions options, OnPrepareCompleteCallback<ObjectDetectorAndTracker> callback)
        {
            Options = options;
            m_implementation.Prepare(inputSource, options, (error) => callback?.Invoke(this, error));
        }


        /// <summary>
        /// Prepare with options and callback gets called once prepare is completed.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        [Obsolete("This method is obsolete. Pass Input source instance as first parameter to Prepare method.", true)]
        public void Prepare(ObjectDetectorAndTrackerOptions options, OnPrepareCompleteCallback<ObjectDetectorAndTracker> callback)
        {
            Options = options;
            m_implementation.Prepare(null, options, (error) => callback?.Invoke(this, error));
        }

        /// <summary>
        /// Process the input source with the provided options. Callback gets called with a result once processing of current input frame is done.
        /// </summary>
        /// <param name="callback"></param>
        public void Process(OnProcessUpdateCallback<ObjectDetectorAndTracker, ObjectDetectorAndTrackerResult> callback)
        {
            m_implementation.Process((result) => callback?.Invoke(this, result));
        }

        /// <summary>
        /// Shutdown to release resources.
        /// </summary>
        /// <param name="callback"></param>
        public void Close(OnCloseCallback<ObjectDetectorAndTracker> callback)
        {
            m_implementation.Close((error) => callback?.Invoke(this, error));
        }
    }
}

