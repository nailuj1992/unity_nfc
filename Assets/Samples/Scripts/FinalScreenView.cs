using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public class FinalScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject scanProcessPanel;
        [SerializeField] private GameObject passportScanPanel;
        [SerializeField] private GameObject correctPassportPanel;
        [SerializeField] private GameObject readScreenGO;
        [SerializeField] private GameObject backgroundScreenGO;
        [SerializeField] private GameObject introScreenGO;

        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject nextScreen;

        public void GoToShowInformation()
        {
            try
            {
                string firstName = PlayerPrefs.GetString("firstName");
                string lastName = PlayerPrefs.GetString("lastName");
                string dateOfBirth = PlayerPrefs.GetString("dateOfBirth");// Date in format dd.MM.yyyy
                string nationality = PlayerPrefs.GetString("nationality");
                string gender = PlayerPrefs.GetString("gender");
                string documentNumber = PlayerPrefs.GetString("documentNumber");
                string documentCode = PlayerPrefs.GetString("documentCode");
                string base64ImageFace = PlayerPrefs.GetString("base64ImageFace");

                string genderParsed = "Unespecified";
                if ("M".Equals(gender, StringComparison.CurrentCultureIgnoreCase))
                {
                    genderParsed = "Male";
                }
                else if ("F".Equals(gender, StringComparison.CurrentCultureIgnoreCase))
                {
                    genderParsed = "Female";
                }

                string document = documentCode + "-" + documentNumber;

                // Purging unnecessary data from PlayerPrefs
                PlayerPrefs.SetString("MRZInfo", null);
                PlayerPrefs.SetString("documentNumber", null);
                PlayerPrefs.SetString("documentCode", null);

                // Saving/keeping relevant data into PlayerPrefs (or 'session storage')
                PlayerPrefs.SetString("gender", genderParsed.ToString());
                PlayerPrefs.SetString("document", document);

                nextScreen.gameObject.SetActive(true);
                panel.gameObject.SetActive(false);
                introScreenGO.gameObject.SetActive(false);
                readScreenGO.gameObject.SetActive(false);
                backgroundScreenGO.gameObject.SetActive(false);
                scanProcessPanel.gameObject.SetActive(false);
                passportScanPanel.gameObject.SetActive(false);
                correctPassportPanel.gameObject.SetActive(false);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}
