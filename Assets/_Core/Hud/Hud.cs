using UnityEngine;

namespace Arkanoid.Hud
{
    public class Hud : MonoBehaviour
    {
        [SerializeField]
        private LivesView _livesView;

        public void SetLives(int lives) => _livesView.SetLives(lives);

        private void Start()
        {
            HeartFactory heartFactory = GameContext.Instance.HeartFactory;
            _livesView.Initialize(heartFactory);
        }
    }
}
