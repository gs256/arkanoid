using Arkanoid.Base.GameStates;
using UnityEngine;

namespace Arkanoid.Base
{
    public class Setup : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private GameStateFactory _gameStateFactory;

        private void Awake()
        {
            _gameStateMachine = GlobalContext.Instance.GameStateMachine;
            _gameStateFactory = GlobalContext.Instance.GameStateFactory;
        }

        private void Start()
        {
            _gameStateMachine.Initialize(_gameStateFactory);
            _gameStateMachine.Enter<InitializationState>();
        }
    }
}
