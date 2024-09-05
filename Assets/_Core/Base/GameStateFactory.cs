using Arkanoid.Base.GameStates;
using Arkanoid.Menu;

namespace Arkanoid.Base
{
    public class GameStateFactory
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly MainMenuProvider _mainMenuProvider;

        public GameStateFactory(GameStateMachine stateMachine, SceneLoader sceneLoader,
            MainMenuProvider mainMenuProvider)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _mainMenuProvider = mainMenuProvider;
        }

        public InitializationState CreateInitializationState()
        {
            return new InitializationState(_stateMachine);
        }

        public MenuState CreateMenuState()
        {
            return new MenuState(_stateMachine, _sceneLoader, _mainMenuProvider);
        }

        public GameState CreateGameState()
        {
            return new GameState(_stateMachine, _sceneLoader);
        }
    }
}
