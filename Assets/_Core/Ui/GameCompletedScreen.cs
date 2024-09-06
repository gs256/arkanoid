using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Ui
{
    public class GameCompletedScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _exitButton;

        private UiController _uiController;

        public void Initialize(UiController uiController)
        {
            _uiController = uiController;
        }

        private void Start()
        {
            _exitButton.onClick.AddListener(_uiController.ExitToMenu);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(_uiController.ExitToMenu);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
