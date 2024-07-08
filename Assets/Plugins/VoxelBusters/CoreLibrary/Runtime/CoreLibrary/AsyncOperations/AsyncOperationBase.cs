using System.Collections;

namespace VoxelBusters.CoreLibrary
{
    public abstract class AsyncOperationBase<T> : IAsyncOperation, IAsyncOperation<T>, IAsyncOperationUpdateHandler
    {
        #region Constructors

        protected AsyncOperationBase()
        {
            // set initial values
            Status          = AsyncOperationStatus.NotStarted;
            IsDone          = false;
            Error           = null;
            OnComplete      = null;
        }

        #endregion

        #region IAsyncOperation implementation

        public AsyncOperationStatus Status { get; private set; }

        public bool IsDone { get; private set; }

        object IAsyncOperation.Result => Result;

        public Error Error { get; private set; }

        public float Progress { get; protected set; }

        public event Callback<IAsyncOperation> OnProgress;

        public event Callback<IAsyncOperation> OnComplete;

        public T Result { get; private set; }

        object IEnumerator.Current => null;

        bool IEnumerator.MoveNext()
        {
            if (AsyncOperationStatus.NotStarted == Status)
            {
                SetStarted();
            }
            
            return !IsDone;
        }

        public virtual void Reset()
        {
            // reset properties
            Status      = AsyncOperationStatus.NotStarted;
            IsDone      = false;
            Error       = null;
            Progress    = 0f;
        }

        #endregion

        #region IAsyncOperationUpdateHandler implemetation

        void IAsyncOperationUpdateHandler.Update()
        {
            // execute start method instructions when operation begins
            if (AsyncOperationStatus.InProgress == Status)
            {
                OnUpdate();
                OnProgress?.Invoke(this);
                return;
            }
        }

        #endregion

        #region Public methods

        public void Start()
        {
            // check whether operation is already started
            if (AsyncOperationStatus.NotStarted != Status)
            {
                DebugLogger.LogWarning(CoreLibraryDomain.Default, "The requested operation could not be started.");
                return;
            }

            SetStarted();
        }

        public void Abort()
        {
            // check whether operation is already completed
            if (AsyncOperationStatus.InProgress != Status)
            {
                DebugLogger.LogWarning(CoreLibraryDomain.Default, "The requested operation could not be cancelled.");
                return;
            }

            OnAbort();
            SetIsCompleted(error: new Error("Async operation was cancelled!"));
        }

        #endregion

        #region Private methods

        private void SetStarted()
        {
            // update status
            Status      = AsyncOperationStatus.InProgress;

            // add object to scheduler
            AsyncOperationManager.ScheduleUpdate(this);

            // invoke state handler method
            OnStart();
        }

        protected virtual void SetIsCompleted(T result = default(T))
        {
            SetIsCompleted(result: result, error: null, status: AsyncOperationStatus.Succeeded);
        }

        protected virtual void SetIsCompleted(Error error)
        {
            Assert.IsArgNotNull(error, nameof(error));
            
            SetIsCompleted(result: default(T), error: error, status: AsyncOperationStatus.Failed);
        }

        private void SetIsCompleted(T result, Error error, AsyncOperationStatus status)
        {
            if (AsyncOperationStatus.InProgress != Status)
            {
                DebugLogger.LogWarning(CoreLibraryDomain.Default, "The requested operation could not be marked as completed.");
                return;
            }

            // remove object from scheduler
            AsyncOperationManager.UnscheduleUpdate(this);

            // update status
            IsDone      = true;
            Result      = result;
            Error       = error;
            Status      = status;
            
            // send event
            OnEnd();
            OnComplete?.Invoke(this);
        }

        #endregion

        #region State methods
        
        protected virtual void OnStart()
        { }

        protected virtual void OnUpdate()
        { }

        protected virtual void OnEnd()
        { }

        protected virtual void OnAbort()
        { }

        #endregion
    }
}