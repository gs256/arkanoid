using System.Collections;
using System.Linq;
using UnityEngine;

namespace Arkanoid
{
    public class LevelManager
    {
        private const float DelayBeforeRestart = 1f;

        private readonly LevelRepository _levelRepository;
        private readonly CoroutineRunner _coroutineRunner;
        private Level _level;

        public LevelManager(LevelRepository levelRepository, CoroutineRunner coroutineRunner)
        {
            _levelRepository = levelRepository;
            _coroutineRunner = coroutineRunner;
        }

        public void LoadFirstLevel()
        {
            Level prefab = _levelRepository.Levels.First();
            Debug.Assert(prefab != null);
            _level = Object.Instantiate(prefab);
            _level.Initialize();
            _level.Died += OnDied;
        }

        public void StartLevel()
        {
            _level.StartLevel();
        }

        public void RestartLevel()
        {
            UnloadLevel();
            LoadFirstLevel();
        }

        public void UnloadLevel()
        {
            _level.Died -= OnDied;
            Object.Destroy(_level.gameObject);
        }

        private void OnDied()
        {
            _coroutineRunner.StartCoroutine(RestartAfterDelayCoroutine());
        }

        private IEnumerator RestartAfterDelayCoroutine()
        {
            yield return new WaitForSeconds(DelayBeforeRestart);
            RestartLevel();
        }
    }
}
