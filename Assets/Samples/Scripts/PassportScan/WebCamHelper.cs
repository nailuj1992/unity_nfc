using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DigitsNFCToolkit.Samples
{
    public class WebCamHelper : MonoBehaviour, ITextureGetter
    {
        [SerializeField] private RawImage background;
        [SerializeField] private AspectRatioFitter fit;

        private WebCamTexture _backCam;
        private bool _camAvailable;
        private int _currentCamIndex;

        public void SetUp()
        {
            SetCamera(0);
        }

        public Texture2D GetCameraTexture()
        {
            if (!_camAvailable)
            {
                return null;
            }

            Texture2D texture = new Texture2D(_backCam.width, _backCam.height, TextureFormat.ARGB32, false);
            texture.SetPixels(_backCam.GetPixels());
            texture.Apply();

            return texture;
        }

        // public Texture2D GetCameraTexture()
        // {
        //     if (!_camAvailable)
        //     {
        //         return null;
        //     }
        //     int videoRotationAngle = _backCam.videoRotationAngle;
        //     
        //     Rect uvRect = background.uvRect;
        //     int x = Mathf.FloorToInt(_backCam.width * uvRect.x);
        //     int y = Mathf.FloorToInt(_backCam.height * uvRect.y);
        //     int width = Mathf.FloorToInt(_backCam.width * uvRect.width);
        //     int height = Mathf.FloorToInt(_backCam.height * uvRect.height);
        //
        //     Texture2D texture;
        //     Color[] colors = _backCam.GetPixels(x, y, width, height);
        //     if (videoRotationAngle is 270 or 90)
        //     {
        //         colors = RotateTexture(colors, width, height, videoRotationAngle);
        //         texture = new Texture2D(height, width, TextureFormat.ARGB32, false);
        //     }
        //     else
        //     {
        //         texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        //     }
        //     texture.SetPixels(colors);
        //     texture.Apply();
        //
        //     return texture;
        // }

        public void FlipCamera()
        {
            SetCamera(_currentCamIndex + 1);
        }

        private void SetCamera(int index)
        {
            int devicesCount = WebCamTexture.devices.Length;
            if (devicesCount == 0)
            {
                Log("No Camera detected");
                _camAvailable = false;
                return;
            }
            _currentCamIndex = index % devicesCount;

            _backCam?.Stop();
            _backCam = new WebCamTexture(WebCamTexture.devices[_currentCamIndex].name);

            if (_backCam == null)
            {
                _camAvailable = false;
                Log("Unable to find back camera");
                return;
            }

            _backCam.Play();
            background.texture = _backCam;
            Refresh();
            _camAvailable = true;
        }

        public void stopCamera()
        {
            _backCam?.Stop();
            _camAvailable = false;
        }

        private void Refresh()
        {
            // float ratio = (float)_backCam.width / _backCam.height;
            // fit.aspectRatio = ratio;

            // int orient = _backCam.videoRotationAngle;
            // background.rectTransform.localEulerAngles = new Vector3(0, 0, -orient);

            float scaleY = _backCam.videoVerticallyMirrored ? -1 : 1f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            // if (orient == 90 || orient == 270)
            // {
            //     background.rectTransform.localScale *= ratio;
            // }
        }

        private Color[] RotateTexture(Color[] originalColors, int width, int height, float angle)
        {
            Color[] rotated = new Color[originalColors.Length];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int newX, newY;
                    switch (angle)
                    {
                        case 90:
                            newX = j;
                            newY = width - 1 - i;
                            break;
                        case 270:
                            newX = height - 1 - j;
                            newY = i;
                            break;
                        default:
                            newX = i;
                            newY = j;
                            break;
                    }
                    rotated[newY * height + newX] = originalColors[j * width + i];
                }
            }

            return rotated;
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}
