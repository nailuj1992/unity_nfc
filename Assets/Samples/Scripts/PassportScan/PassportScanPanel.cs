using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace DigitsNFCToolkit.Samples
{
    public class PassportScanPanel : MonoBehaviour
    {
        [SerializeField] private WebCamHelper webCamHelper;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject nextScreen;

        private CancellationTokenSource CancellationTokenSource { get; set; }

        private async void OnEnable()
        {
            await Show();
        }

        private async void OnDisable()
        {
            await Hide();
        }

        public void Dispose()
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
            CancellationTokenSource = null;
        }

        public async Task Show()
        {
            panel.SetActive(true);
            CancellationTokenSource = new CancellationTokenSource();
            RecognizeAsync(CancellationTokenSource.Token);
        }

        public virtual async Task Hide()
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
            CancellationTokenSource = null;

            panel.SetActive(false);
        }

        private async void RecognizeAsync(CancellationToken cancellationToken)
        {
            webCamHelper.SetUp();
            await PassportScanValues.GetInstance().Model.Recognize(cancellationToken, webCamHelper);
            webCamHelper.stopCamera();
            Debug.Log("MRZ: " + PassportScanValues.GetInstance().Model.MRZString);
            panel.SetActive(false);
            nextScreen.SetActive(true);
        }
    }
}
