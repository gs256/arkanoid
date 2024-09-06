using UnityEngine;
using UnityEngine.EventSystems;

namespace Arkanoid.Ui
{
    public class GameArea : MonoBehaviour, IPointerClickHandler
    {
        private UiController _uiController;

        public void Initialize(UiController uiController)
        {
            _uiController = uiController;
        }

        public void OnPointerClick(PointerEventData _)
        {
            _uiController.StartGame();
        }
    }
}
