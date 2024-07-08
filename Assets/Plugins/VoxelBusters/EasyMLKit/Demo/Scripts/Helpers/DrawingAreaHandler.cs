using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VoxelBusters.EasyMLKit.Demo
{
    public class DrawingAreaHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField]
        private GameObject m_brushPrefab;
        private IDrawingStrokeListener m_strokeListener;
        private DrawingStroke m_currentStroke;
        private Vector2 m_dimensions;
        private List<GameObject> m_paintedObjects = new List<GameObject>();
        private List<GameObject> m_freeObjects = new List<GameObject>();

        private void Awake()
        {
            m_dimensions = ((RectTransform)transform).sizeDelta;
        }

        public void SetStorkeListener(IDrawingStrokeListener listener)
        {
            m_strokeListener = listener;
        }

        public float GetAreaWidth()
        {
            return m_dimensions.x;
        }

        public float GetAreaHeight()
        {
            return m_dimensions.y;
        }

        public void ClearDrawing()
        {
            foreach(GameObject each in m_paintedObjects)
            {
                each.SetActive(false);
                m_freeObjects.Add(each);
            }

            m_paintedObjects.Clear();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            m_currentStroke = new DrawingStroke();
            UpdateStroke(m_currentStroke, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateStroke(m_currentStroke, eventData);
            Debug.Log("Event data : " + eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UpdateStroke(m_currentStroke, eventData);

            if(m_strokeListener != null)
            {
                m_strokeListener.OnNewStroke(m_currentStroke);
            }
        }

        private void UpdateStroke(DrawingStroke stroke, PointerEventData eventData)
        {
            stroke.AddPoint(new DrawingPoint(eventData.position.x, GetAreaHeight() - eventData.position.y, (long)Time.time));
            GameObject paintObject = GetPaintObject();
            paintObject.SetActive(true);
            paintObject.transform.position = eventData.position;
            m_paintedObjects.Add(paintObject);
        }

        private GameObject GetPaintObject()
        {
            GameObject gameObject;

            if (m_freeObjects.Count > 0)
            {
                gameObject = m_freeObjects[m_freeObjects.Count - 1];
                m_freeObjects.Remove(gameObject);
            }
            else
            {
                gameObject = Instantiate(m_brushPrefab, transform);
            }

            return gameObject;
        }
    }
}