using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Ui
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _resumeButton;

        [SerializeField]
        private Button _exitButton;

        private UiController _uiController;

        public void Initialize(UiController uiController)
        {
            _uiController = uiController;
        }

        private void Start()
        {
            _resumeButton.onClick.AddListener(Resume);
            _exitButton.onClick.AddListener(ExitToMenu);
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveListener(Resume);
            _exitButton.onClick.RemoveListener(ExitToMenu);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Resume()
        {
            _uiController.ResumeGame();
            Hide();
        }

        private void ExitToMenu()
        {
            _uiController.ExitToMenu();
        }
    }
}
