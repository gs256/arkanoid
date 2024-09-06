using System;

namespace Arkanoid.Ui
{
    public class PlayerView : IDisposable
    {
        private readonly Player _player;
        private readonly UiController _uiController;

        public PlayerView(Player player, UiController uiController)
        {
            _player = player;
            _uiController = uiController;

            _player.ScoreChanged += UpdateScore;
            _player.LivesChanged += UpdateLives;
        }

        public void Dispose()
        {
            _player.ScoreChanged -= UpdateScore;
            _player.LivesChanged -= UpdateLives;
        }

        private void UpdateLives(int lives)
        {
            _uiController.ShowLives(lives);
        }

        private void UpdateScore(int points)
        {
            _uiController.ShowScore(points);
        }
    }
}
