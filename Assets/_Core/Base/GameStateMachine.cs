using System;
using System.Collections.Generic;
using Arkanoid.Base.GameStates;

namespace Arkanoid.Base
{
    public class GameStateMachine
    {
        private GameStateFactory _gameStateFactory;

        private readonly Dictionary<Type, IGameState> _states = new();
        private IGameState _currentState;

        public void Initialize(GameStateFactory gameStateFactory)
        {
            _gameStateFactory = gameStateFactory;
            _states[typeof(InitializationState)] = _gameStateFactory.CreateInitializationState();
            _states[typeof(MenuState)] = _gameStateFactory.CreateMenuState();
            _states[typeof(GameState)] = _gameStateFactory.CreateGameState();
        }

        public void Enter<T>()
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }
    }
}
