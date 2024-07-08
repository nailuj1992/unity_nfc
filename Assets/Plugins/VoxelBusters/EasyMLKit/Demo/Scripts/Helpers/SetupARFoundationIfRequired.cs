using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

namespace VoxelBusters.EasyMLKit.Demo
{
    public class SetupARFoundationIfRequired :  MonoBehaviour
    {
        [SerializeField]
        private GameObject m_arFoundationSetupPrefab;

        public void Awake()
        {
#if EASY_ML_KIT_SUPPORT_AR_FOUNDATION
            Instantiate(m_arFoundationSetupPrefab);
#endif
        }
    }
}