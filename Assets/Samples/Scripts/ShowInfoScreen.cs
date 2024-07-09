using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DigitsNFCToolkit.Samples
{
    public class ShowInfoScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text FirstNameText;
        [SerializeField] private TMP_Text LastNameText;
        [SerializeField] private TMP_Text DateBirthText;
        [SerializeField] private TMP_Text NationalityText;
        [SerializeField] private TMP_Text GenderText;
        [SerializeField] private TMP_Text DocumentText;
        [SerializeField] private RawImage FaceImage;

        private void OnEnable()
        {
            try
            {
                string firstName = PlayerPrefs.GetString("firstName");
                string lastName = PlayerPrefs.GetString("lastName");
                string dateOfBirth = PlayerPrefs.GetString("dateOfBirth");// Date in format dd.MM.yyyy
                string nationality = PlayerPrefs.GetString("nationality");
                string gender = PlayerPrefs.GetString("gender");
                string document = PlayerPrefs.GetString("document");
                string base64ImageFace = PlayerPrefs.GetString("base64ImageFace");
                int widthImageFace = PlayerPrefs.GetInt("widthImageFace");
                int heightImageFace = PlayerPrefs.GetInt("heightImageFace");

                FirstNameText.text = firstName;
                LastNameText.text = lastName;
                DateBirthText.text = dateOfBirth;
                NationalityText.text = nationality;
                GenderText.text = gender;
                DocumentText.text = document;

                byte[] byteBuffer = Convert.FromBase64String(base64ImageFace);
                Texture2D texture = new Texture2D(widthImageFace, heightImageFace, TextureFormat.RGBA32, false);

                // Load byte data into the texture
                texture.LoadRawTextureData(byteBuffer);
                texture.Apply();

                FaceImage.texture = texture;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        private void OnDisable()
        {
            FirstNameText.text = "";
            LastNameText.text = "";
            DateBirthText.text = "";
            NationalityText.text = "";
            GenderText.text = "";
            DocumentText.text = "";
        }

        public void ResetPlayerPrefs()
        {
            PlayerPrefs.SetString("firstName", null);
            PlayerPrefs.SetString("lastName", null);
            PlayerPrefs.SetString("dateOfBirth", null);
            PlayerPrefs.SetString("nationality", null);
            PlayerPrefs.SetString("gender", null);
            PlayerPrefs.SetString("document", null);
            PlayerPrefs.SetString("base64ImageFace", null);
            PlayerPrefs.SetInt("widthImageFace", 0);
            PlayerPrefs.SetInt("heightImageFace", 0);
        }
    }
}
