using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit.Demo
{
    public class ObjectOverlayController : SingletonBehaviour<ObjectOverlayController>
    {
        private List<ObjectOverlay> m_pool = new List<ObjectOverlay>();
        private List<ObjectOverlay> m_currentOverlays = new List<ObjectOverlay>();
        private Canvas m_cachedCanvas;

        private void Awake()
        {
            m_cachedCanvas = GameObject.FindObjectOfType<Canvas>();
        }

        public void ClearAll()
        {
            foreach (ObjectOverlay each in m_currentOverlays)
            {
                each.gameObject.SetActive(false);
                m_pool.Add(each);
            }

            m_currentOverlays.Clear();
        }

        public void ShowOverlay(Rect rectInScreenSpace, string label)
        {
            ObjectOverlay overlay = GetFreeOverlay();
            overlay.SetRect(m_cachedCanvas.transform as RectTransform, rectInScreenSpace);
            overlay.SetLabel(label);
        }

        public ObjectOverlay GetFreeOverlay()
        {
            ObjectOverlay overlay;
            if (m_pool.Count == 0)
            {
                //ObjectOverlay overlay;//Instantiate
                overlay = (Instantiate(Resources.Load("EasyMLKitObjectOverlay"), m_cachedCanvas.transform) as GameObject).GetComponent<ObjectOverlay>();
            }
            else
            {
                overlay = m_pool[m_pool.Count - 1];
                m_pool.Remove(overlay);
            }

            overlay.gameObject.SetActive(true);
            m_currentOverlays.Add(overlay);
            return overlay;
        }
    }
}
