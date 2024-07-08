using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.CoreLibrary
{
    public class SettingsObject : ScriptableObject
    {
        #region Events

        public event Callback OnSettingsUpdated;

        #endregion

        #region Private methods

        protected virtual void OnValidate()
        {
            OnSettingsUpdated?.Invoke();
        }

        #endregion
    }
}