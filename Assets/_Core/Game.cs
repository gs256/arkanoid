using Arkanoid.Base;
using Arkanoid.Base.GameStates;
using Arkanoid.Levels;
using UnityEngine;

namespace Arkanoid
{
    public class Game : MonoBehaviour
    {
        private LevelManager _levelManager;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _levelManager = GlobalContext.Instance.LevelManager;
            _gameStateMachine = GlobalContext.Instance.GameStateMachine;
        }

        private void Start()
        {
            _levelManager.CompletedAllLevels += OnCompletedAllLevels;
            _levelManager.LoadFirstLevel();
        }

        private void OnDestroy()
        {
            _levelManager.CompletedAllLevels -= OnCompletedAllLevels;
        }

        private void OnCompletedAllLevels()
        {
            _gameStateMachine.Enter<MenuState>();
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
