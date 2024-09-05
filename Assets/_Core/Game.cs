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
        private Player _player;
        private Hud.Hud _hud;

        private void Awake()
        {
            _levelManager = GlobalContext.Instance.LevelManager;
            _gameStateMachine = GlobalContext.Instance.GameStateMachine;
            _hud = GameContext.Instance.Hud;

            _player = new Player();
        }

        private void Start()
        {
            _levelManager.CompletedAllLevels += OnCompletedAllLevels;
            _levelManager.Died += OnDied;
            _levelManager.LoadFirstLevel();

            // FIXME
            _hud.SetLives(3);
        }

        private void OnDestroy()
        {
            _levelManager.CompletedAllLevels -= OnCompletedAllLevels;
            _levelManager.Died -= OnDied;
        }

        private void OnDied()
        {
            _player.DecreaseLives();

            if (_player.Lives > 0)
                _levelManager.RestartCurrentLevel();
            else
                _gameStateMachine.Enter<MenuState>();
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
