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

        public void LoadGame()
        {
            _levelManager.CompletedAllLevels += OnCompletedAllLevels;
            _levelManager.Lost += OnLost;

            _player.Initialize();
            _levelManager.LoadFirstLevel();
            _uiController.ShowLives(_player.Lives);
            Time.timeScale = 1f;
        }

        public void StartGame()
        {
            _levelManager.StartLevel();
        }

        public void Pause()
        {
            _levelManager.PauseLevel();
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            _levelManager.ResumeLevel();
            Time.timeScale = 1f;
        }

        public void Dispose()
        {
            _levelManager.CompletedAllLevels -= OnCompletedAllLevels;
            _levelManager.Lost -= OnLost;
        }

        public void Revive()
        {
            _player.Revive();
            _levelManager.ReviveCurrentLevel();
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            _player.Initialize();
            _levelManager.RestartCompletely();
            _uiController.ShowLives(_player.Lives);
        }

        private void OnLost()
        {
            _uiController.ShowGameOver();
        }

        private void OnCompletedAllLevels()
        {
            _uiController.ShowGameCompleted();
        }
    }
}
