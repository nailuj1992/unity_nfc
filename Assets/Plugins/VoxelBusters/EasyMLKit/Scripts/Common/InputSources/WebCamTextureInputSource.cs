using System;
using System.Collections;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    internal class WebCamTextureInputSource : IImageInputSource
    {
        private WebCamTexture m_webCamTexture;

        public event Action CameraFrameReceived;

        public WebCamTexture Texture
        {
            get
            {
                return m_webCamTexture;
            }
        }


        public WebCamTextureInputSource(WebCamTexture webCamTexture)
        {
            m_webCamTexture = webCamTexture;
            SurrogateCoroutine.StartCoroutine(TriggerFrameUpdate());
        }

        private IEnumerator TriggerFrameUpdate()
        {
            while(true)
            {
                yield return new WaitForEndOfFrame();
                if (m_webCamTexture.didUpdateThisFrame && CameraFrameReceived != null)
                    CameraFrameReceived();
            }
        }

        public void Close()
        {
            SurrogateCoroutine.StopCoroutine(TriggerFrameUpdate());
            m_webCamTexture.Stop();
        }

        public float GetWidth()
        {
            return Texture.width;
        }

        public float GetHeight()
        {
            return Texture.height;
        }
    }
}
