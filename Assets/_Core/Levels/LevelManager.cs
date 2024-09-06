using System;
using System.Collections;
using Arkanoid.Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Arkanoid.Levels
{
    public class LevelManager
    {
        public event Action CompletedAllLevels;
        public event Action Died;

        private const float DelayBeforeRestart = 1f;

        private readonly LevelRepository _levelRepository;
        private readonly CoroutineRunner _coroutineRunner;
        private readonly UiController _uiController;
        private readonly LevelFactory _levelFactory;
        private Level _level;
        private int _currentIndex;

        public LevelManager(LevelRepository levelRepository, CoroutineRunner coroutineRunner,
            UiController uiController, LevelFactory levelFactory)
        {
            _levelRepository = levelRepository;
            _coroutineRunner = coroutineRunner;
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

        public void RestartCompletely()
        {
            UnloadCurrentLevel();
            LoadFirstLevel();
        }

        public void UnloadCurrentLevel()
        {
            _level.Died -= OnDied;
            _level.Completed -= OnCompleted;
            Object.Destroy(_level.gameObject);
        }

        private void LoadLevelWithIndex(int index)
        {
            Debug.Assert(_levelRepository.Levels.Count > index);
            _level = _levelFactory.Create(levelIndex: index);
            _level.Died += OnDied;
            _level.Completed += OnCompleted;
            _currentIndex = index;
            _uiController.ShowLevel(_currentIndex + 1);
        }

        private void OnDied()
        {
            _coroutineRunner.StartCoroutine(ProcessDeathAfterDelayCoroutine());
        }

        private void OnCompleted()
        {
            _coroutineRunner.StartCoroutine(ProcessLevelCompletionAfterDelay());
        }

        private void ProcessLevelCompletion()
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

        private IEnumerator ProcessDeathAfterDelayCoroutine()
        {
            yield return new WaitForSeconds(DelayBeforeRestart);
            Died?.Invoke();
        }

        private IEnumerator ProcessLevelCompletionAfterDelay()
        {
            yield return new WaitForSeconds(DelayBeforeRestart);
            ProcessLevelCompletion();
        }
    }
}
