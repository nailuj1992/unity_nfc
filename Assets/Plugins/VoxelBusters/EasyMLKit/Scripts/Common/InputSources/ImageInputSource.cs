using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.CoreLibrary;

namespace VoxelBusters.EasyMLKit
{
    /// <summary>
    /// Input source which takes Image in the form of Texture or byte array
    /// </summary>
    public class ImageInputSource : IImageInputSource
    {
        private byte[] m_data;
        private string m_mimeType;
        private float m_width;
        private float m_height;

        /// <summary>
        /// Pass texture as input. This gets encoded to png internally and passed to the processor
        /// </summary>
        /// <param name="texture"></param>
        public ImageInputSource(Texture2D texture)
        {
            if (!texture.isReadable)
            {
                throw new System.Exception("Texture is not readable. You need to mark it in your texture import settings as readable and without any compression on.");
            }

            m_data = texture.EncodeToPNG();
            m_mimeType = MimeType.kPNGImage;
            m_width = texture.width;
            m_height = texture.height;
        }

        /// <summary>
        /// Pass byte array with mime type as input. Ex: Pass jpeg byte array with image/jpeg as mime type
        /// </summary>
        /// <param name="data"></param>
        /// <param name="mimeType"></param>
        public ImageInputSource(byte[] data, float width, float height, string mimeType)
        {
            this.m_data = data;
            this.m_mimeType = mimeType;
            m_width = width;
            m_height = height;
        }

        public void Close()
        {
            m_data = null;
        }

        /// <summary>
        /// Get undelying bytes for this input
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            return m_data;
        }

        /// <summary>
        /// Get mime type of this data
        /// </summary>
        /// <returns></returns>
        public string GetMimeType()
        {
            return m_mimeType;
        }

        public float GetWidth()
        {
            return m_width;
        }

        public float GetHeight()
        {
            return m_height;
        }
    }
}
