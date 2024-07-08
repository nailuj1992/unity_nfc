using UnityEngine;
using UnityEngine.UI;

namespace VoxelBusters.EasyMLKit
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class PointsOverlay : Graphic
    {
        protected RectTransform m_cachedRectTransform;

        [SerializeField]
        private float m_pointSize = 5;

        [SerializeField]
        private Texture m_texture;

        [SerializeField]
        private bool m_drawBoundingBox;

        private Vector2[] m_fullQuadUvs = new Vector2[]
        {
            new Vector2(0,0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0)
        };

        public override Texture mainTexture
        {
            get
            {
                return m_texture ?? s_WhiteTexture;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_cachedRectTransform = transform as RectTransform;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            AddPoints(vh);
        }

        //Override this for adding points
        protected virtual void AddPoints(VertexHelper vh) 
        {
            AddPoint(vh, m_cachedRectTransform.anchoredPosition);
        }

        protected void AddPoint(VertexHelper vh, Vector2 point)
        {
            AddQuad(vh, GetVerticesFromPoint(point), Random.ColorHSV(), m_fullQuadUvs);
        }

        private Vector3[] GetVerticesFromPoint(Vector2 point)
        {
            Vector3[] vertices = new Vector3[4];

            vertices[0] = new Vector3(point.x - m_pointSize, point.y - m_pointSize);
            vertices[1] = new Vector3(point.x - m_pointSize, point.y + m_pointSize);
            vertices[2] = new Vector3(point.x + m_pointSize, point.y + m_pointSize);
            vertices[3] = new Vector3(point.x + m_pointSize, point.y - m_pointSize);

            return vertices;
        }

        private void AddQuad(VertexHelper vh, Vector3[] vertices, Color color, Vector2[] uvs)
        {
            UIVertex[] uiVertices = new UIVertex[4];

            for (int index = 0; index < uiVertices.Length; index++)
            {
                UIVertex uiVertex = UIVertex.simpleVert;

                uiVertex.position = vertices[index];
                uiVertex.color = color;
                if(uvs != null)
                {
                    uiVertex.uv0 = uvs[index];
                }

                uiVertices[index] = uiVertex;
            }

            vh.AddUIVertexQuad(uiVertices);
        }
    }
}