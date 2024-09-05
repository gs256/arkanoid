using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Arkanoid.Levels
{
    public class LevelManager
    {
        public event Action CompletedAllLevels;

        private const float DelayBeforeRestart = 1f;

        private readonly LevelRepository _levelRepository;
        private readonly CoroutineRunner _coroutineRunner;
        private Level _level;
        private int _currentIndex;

        public LevelManager(LevelRepository levelRepository, CoroutineRunner coroutineRunner)
        {
            _levelRepository = levelRepository;
            _coroutineRunner = coroutineRunner;
        }

        public void LoadFirstLevel()
        {
            LoadLevelWithIndex(0);
        }

        public void StartLevel()
        {
            _level.StartLevel();
        }

        public void RestartLevel()
        {
            UnloadCurrentLevel();
            LoadLevelWithIndex(_currentIndex);
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
            _level = Object.Instantiate(_levelRepository.Levels[index]);
            _level.Initialize();
            _level.Died += OnDied;
            _level.Completed += OnCompleted;
            _currentIndex = index;
        }

        private void OnDied()
        {
            _coroutineRunner.StartCoroutine(RestartAfterDelayCoroutine());
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

        private IEnumerator RestartAfterDelayCoroutine()
        {
            yield return new WaitForSeconds(DelayBeforeRestart);
            RestartLevel();
        }

        private IEnumerator ProcessLevelCompletionAfterDelay()
        {
            yield return new WaitForSeconds(DelayBeforeRestart);
            ProcessLevelCompletion();
        }
    }
}
