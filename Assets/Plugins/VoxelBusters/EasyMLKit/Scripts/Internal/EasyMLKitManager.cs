using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Internal
{
    public class EasyMLKitManager : PrivateSingletonBehaviour<EasyMLKitManager>
    {
        #region Static methods

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnLoad()
        {
#pragma warning disable 
            var singleton = GetSingleton();
#pragma warning restore 
        }

        #endregion

        #region Unity methods

        protected override void OnSingletonAwake()
        {
            base.OnSingletonAwake();

            CallbackDispatcher.Initialize();

            //Initialise EasyMLKit Settings instance
        }

        #endregion
    }
}