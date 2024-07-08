using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.CoreLibrary
{
    internal class AsyncOperationManager : PrivateSingletonBehaviour<AsyncOperationManager>
    {
        #region Static fields

        private     static  List<IAsyncOperationUpdateHandler>  s_activeObjects     = new List<IAsyncOperationUpdateHandler>();

        private     static  List<IAsyncOperationUpdateHandler>  s_completedObjects  = new List<IAsyncOperationUpdateHandler>();
        
        #endregion

        #region Static methods

        public static void ScheduleUpdate(IAsyncOperationUpdateHandler updateHandler)
        {
            Assert.IsArgNotNull(updateHandler, nameof(updateHandler));

            CreateObjectIfRequired();

            // add object to scheduler
            s_activeObjects.Add(updateHandler);
        }

        public static void UnscheduleUpdate(IAsyncOperationUpdateHandler updateHandler)
        {
            Assert.IsArgNotNull(updateHandler, nameof(updateHandler));

            // add object to remove list
            s_completedObjects.Add(updateHandler);
        }

        private static void CreateObjectIfRequired()
        {
            // create object if required
            if (!IsSingletonActive)
            {
                GetSingleton();
            }
        }

        #endregion

        #region Unity methods

        private void Update()
        {
            RemoveCompletedObjects();
            TickActiveObjects();
        }

        #endregion

        #region Private methods

        private void RemoveCompletedObjects()
        {
            // remove completed objects
            for (int iter = 0; iter < s_completedObjects.Count; iter++)
            {
                var     updateHandler   = s_completedObjects[iter];
                s_activeObjects.Remove(updateHandler);
            }

            s_completedObjects.Clear();
        }

        private void TickActiveObjects()
        {
            // call update function for active operations
            for (int iter = 0; iter < s_activeObjects.Count; iter++)
            {
                var     updateHandler   = s_activeObjects[iter];
                updateHandler.Update();
            }
        }
         
        #endregion
    }
}