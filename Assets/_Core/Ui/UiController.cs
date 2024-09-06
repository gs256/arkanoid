using Arkanoid.Base;
using Arkanoid.Base.GameStates;
using Arkanoid.Ui.Lives;
using UnityEngine;

namespace Arkanoid.Ui
{
    public class UiController : MonoBehaviour
    {
        [SerializeField]
        private LivesView _livesView;

        [SerializeField]
        private GameOverScreen _gameOverScreen;

        [SerializeField]
        private Game _game;

        private GameStateMachine _gameStateMachine;

        public void ShowLives(int lives) => _livesView.SetLives(lives);
        public void ShowGameOver() => _gameOverScreen.Show();

        public void Revive() => _game.Revive();
        public void ExitToMenu() => _gameStateMachine.Enter<MenuState>();
        public void RestartGame() => _game.Restart();

        private void Awake()
        {
            HeartFactory heartFactory = GameContext.Instance.HeartFactory;
            _gameStateMachine = GlobalContext.Instance.GameStateMachine;

            _livesView.Initialize(heartFactory);
            _gameOverScreen.Initialize(this);
            _gameOverScreen.Hide();
        }
    }
}
