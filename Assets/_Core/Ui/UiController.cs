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
        private LevelView _levelView;

        [SerializeField]
        private ScoreView _scoreView;

        [SerializeField]
        private GameOverScreen _gameOverScreen;

        [SerializeField]
        private GameCompletedScreen _gameCompletedScreen;

        [SerializeField]
        private Game _game;

        [SerializeField]
        private GameArea _gameArea;

        [SerializeField]
        private PauseMenu _pauseMenu;

        [SerializeField]
        private PauseButton _pauseButton;

        private GameStateMachine _gameStateMachine;

        public void ShowLives(int lives) => _livesView.SetLives(lives);
        public void ShowLevel(int levelNumber) => _levelView.ShowLevel(levelNumber);
        public void ShowScore(int score) => _scoreView.ShowScore(score);
        public void ShowGameOver() => _gameOverScreen.Show();
        public void ShowGameCompleted() => _gameCompletedScreen.Show();
        public void ShowPauseMenu() => _pauseMenu.Show();

        public void StartGame() => _game.StartGame();
        public void Revive() => _game.Revive();
        public void ExitToMenu() => _gameStateMachine.Enter<MenuState>();
        public void RestartGame() => _game.Restart();
        public void PauseGame() => _game.Pause();
        public void ResumeGame() => _game.Resume();

        private void Awake()
        {
            HeartFactory heartFactory = GameContext.Instance.HeartFactory;
            _gameStateMachine = GlobalContext.Instance.GameStateMachine;

            _livesView.Initialize(heartFactory);
            _gameOverScreen.Initialize(this);
            _gameCompletedScreen.Initialize(this);
            _gameArea.Initialize(this);
            _pauseMenu.Initialize(this);
            _pauseButton.Initialize(this);
            _gameOverScreen.Hide();
            _gameCompletedScreen.Hide();
            _pauseMenu.Hide();
        }
    }
}
