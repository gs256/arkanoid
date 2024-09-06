using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Ui
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _reviveButton;

        [SerializeField]
        private Button _tryAgainButton;

        [SerializeField]
        private Button _exitButton;

        private UiController _uiController;

        public void Initialize(UiController uiController)
        {
            _uiController = uiController;
        }

        private void Start()
        {
            _reviveButton.onClick.AddListener(Revive);
            _tryAgainButton.onClick.AddListener(TryAgain);
            _exitButton.onClick.AddListener(ExitToMenu);
        }

        private void OnDestroy()
        {
            _reviveButton.onClick.RemoveListener(Revive);
            _tryAgainButton.onClick.RemoveListener(TryAgain);
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

        private void Revive()
        {
            _uiController.Revive();
            Hide();
        }

        private void TryAgain()
        {
            _uiController.RestartGame();
            Hide();
        }

        private void ExitToMenu()
        {
            _uiController.ExitToMenu();
        }
    }
}
