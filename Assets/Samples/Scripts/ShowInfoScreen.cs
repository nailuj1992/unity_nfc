using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        private void OnEnable()
        {
            string firstName = PlayerPrefs.GetString("firstName");
            string lastName = PlayerPrefs.GetString("lastName");
            string dateOfBirth = PlayerPrefs.GetString("dateOfBirth");// Date in format dd.MM.yyyy
            string nationality = PlayerPrefs.GetString("nationality");
            string gender = PlayerPrefs.GetString("gender");
            string document = PlayerPrefs.GetString("document");
            string base64ImageFace = PlayerPrefs.GetString("base64ImageFace");

            FirstNameText.text = firstName;
            LastNameText.text = lastName;
            DateBirthText.text = dateOfBirth;
            NationalityText.text = nationality;
            GenderText.text = gender;
            DocumentText.text = document;
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
        }
    }
}
