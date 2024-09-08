using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Ui.Help
{
    public class HelpScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _closeButton;

        private UiController _uiController;

        public void Initialize(UiController uiController)
        {
            _uiController = uiController;
        }

        private void Start()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Close()
        {
            _uiController.ResumeGame();
            gameObject.SetActive(false);
        }
    }
}
