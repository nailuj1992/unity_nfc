using System;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public class CorrectPassportPanel : MonoBehaviour
    {
        [field: SerializeField] private TMP_InputField MrzInputField { get; set; }
        [SerializeField] private GameObject panel;

        private async void OnEnable()
        {
            await Show();
        }

        private async void OnDisable()
        {
            await Hide();
        }

        public async Task Show(params object[] args)
        {
            panel.SetActive(true);
            MrzInputField.text = PassportScanValues.GetInstance().Model.MRZString;
            MrzInputField.onSubmit.AddListener(OnMrzSubmit);
        }

        public async Task Hide()
        {
            MrzInputField.onSubmit.RemoveListener(OnMrzSubmit);
            panel.SetActive(false);
        }

        private void OnMrzSubmit(string text)
        {
            PassportScanValues.GetInstance().Model.MRZString = text;
        }

        public void GoToNFCScan()
        {
            PlayerPrefs.SetString("MRZInfo", MrzInputField.text);
        }
    }
}
