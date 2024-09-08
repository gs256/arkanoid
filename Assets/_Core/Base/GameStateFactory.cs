using Arkanoid.Base.GameStates;
using Arkanoid.Base.Scenes;

namespace Arkanoid.Base
{
    public class GameStateFactory
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public GameStateFactory(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public InitializationState CreateInitializationState()
        {
            return new InitializationState(_stateMachine);
        }

        public MenuState CreateMenuState()
        {
            return new MenuState(_stateMachine, _sceneLoader);
        }

        public GameState CreateGameState()
        {
            return new GameState(_stateMachine, _sceneLoader);
        }
    }
}
