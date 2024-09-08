using Arkanoid.Base;
using Arkanoid.Base.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _exitButton;

        private GameStateMachine _gameStateMachine;

        public void Initialize(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(Exit);
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(Exit);
        }

        private void StartGame()
        {
            _gameStateMachine.Enter<GameState>();
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
