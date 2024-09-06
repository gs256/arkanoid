using System;
using Arkanoid.Base;
using Arkanoid.Levels;
using Arkanoid.Ui;
using UnityEngine;

namespace Arkanoid
{
    public class Game : MonoBehaviour, IDisposable
    {
        private LevelManager _levelManager;
        private Player _player;
        private UiController _uiController;

        private void Awake()
        {
            _player = GlobalContext.Instance.Player;
            _levelManager = GameContext.Instance.LevelManager;
            _uiController = GameContext.Instance.UiController;
        }

        public void StartGame()
        {
            _levelManager.CompletedAllLevels += OnCompletedAllLevels;
            _levelManager.Died += OnDied;

            _player.Initialize();
            _levelManager.LoadFirstLevel();
            _uiController.ShowLives(_player.Lives);
        }

        public void Dispose()
        {
            _levelManager.CompletedAllLevels -= OnCompletedAllLevels;
            _levelManager.Died -= OnDied;
        }

        public void Revive()
        {
            _player.Revive();
            _uiController.ShowLives(_player.Lives);
            _levelManager.RestartCurrentLevel();
        }

        public void Restart()
        {
            _player.Initialize();
            _levelManager.RestartCompletely();
            _uiController.ShowLives(_player.Lives);
        }

        private void OnDied()
        {
            _player.DecreaseLives();
            _uiController.ShowLives(_player.Lives);

            if (_player.Lives > 0)
                _levelManager.RestartCurrentLevel();
            else
                _uiController.ShowGameOver();
        }

        private void OnCompletedAllLevels()
        {
            _uiController.ShowGameCompleted();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _levelManager.StartLevel();
            }
        }
    }
}
