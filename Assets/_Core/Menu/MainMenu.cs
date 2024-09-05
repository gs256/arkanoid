using System;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public event Action PlayClicked;
        public event Action ExitClicked;

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _exitButton;

        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayClicked);
            _exitButton.onClick.RemoveListener(OnExitClicked);
        }

        private void OnPlayClicked()
        {
            PlayClicked?.Invoke();
        }

        private void OnExitClicked()
        {
            ExitClicked?.Invoke();
        }
    }
}
