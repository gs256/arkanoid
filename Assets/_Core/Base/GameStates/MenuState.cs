using Arkanoid.Menu;

namespace Arkanoid.Base.GameStates
{
    public class MenuState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly MainMenuProvider _mainMenuProvider;

        public MenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, MainMenuProvider mainMenuProvider)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _mainMenuProvider = mainMenuProvider;
        }

        public void Enter()
        {
            _sceneLoader.LoadMenu(OnMenuLoaded);
        }

        public void Exit()
        {
            _mainMenuProvider.MainMenu.PlayClicked -= StartGame;
            _sceneLoader.UnloadMenu(callback: null);
        }

        private void OnMenuLoaded()
        {
            _mainMenuProvider.MainMenu.PlayClicked += StartGame;
        }

        private void StartGame()
        {
            _stateMachine.Enter<GameState>();
        }
    }
}
