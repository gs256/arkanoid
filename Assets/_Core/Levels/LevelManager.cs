using System;
using Arkanoid.Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Arkanoid.Levels
{
    public class LevelManager
    {
        public event Action CompletedAllLevels;
        public event Action Lost;

        private readonly LevelRepository _levelRepository;
        private readonly UiController _uiController;
        private readonly LevelFactory _levelFactory;
        private Level _level;
        private int _currentIndex;

        public LevelManager(LevelRepository levelRepository, UiController uiController, LevelFactory levelFactory)
        {
            _levelRepository = levelRepository;
            _uiController = uiController;
            _levelFactory = levelFactory;
        }

        public void LoadFirstLevel()
        {
            LoadLevelWithIndex(0);
        }

        public void StartLevel()
        {
            _level.StartLevel();
        }

        public void RestartCurrentLevel()
        {
            UnloadCurrentLevel();
            LoadLevelWithIndex(_currentIndex);
        }

        public void ReviveCurrentLevel()
        {
            _level.Revive();
        }

        public void RestartCompletely()
        {
            UnloadCurrentLevel();
            LoadFirstLevel();
        }

        private void LoadLevelWithIndex(int index)
        {
            Debug.Assert(_levelRepository.Levels.Count > index);
            _level = _levelFactory.Create(levelIndex: index);
            _level.Completed += OnCompleted;
            _level.Lost += OnLost;
            _currentIndex = index;
            _uiController.ShowLevel(_currentIndex + 1);
        }

        public void UnloadCurrentLevel()
        {
            _level.Completed -= OnCompleted;
            _level.Lost -= OnLost;
            Object.Destroy(_level.gameObject);
        }

        private void OnLost()
        {
            Lost?.Invoke();
        }

        private void OnCompleted()
        {
            if (_levelRepository.Levels.Count == _currentIndex + 1)
            {
                CompletedAllLevels?.Invoke();
            }
            else
            {
                UnloadCurrentLevel();
                LoadLevelWithIndex(_currentIndex + 1);
            }
        }
    }
}
