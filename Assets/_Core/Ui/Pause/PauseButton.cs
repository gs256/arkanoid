using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Ui.Pause
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private UiController _uiController;

        public void Initialize(UiController uiController)
        {
            _uiController = uiController;
        }

        private void Start()
        {
            _button.onClick.AddListener(Pause);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Pause);
        }

        private void Pause()
        {
            _uiController.PauseGame();
            _uiController.ShowPauseMenu();
        }
    }
}
