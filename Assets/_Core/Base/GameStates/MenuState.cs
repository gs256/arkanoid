using Arkanoid.Base.Scenes;

namespace Arkanoid.Base.GameStates
{
    public class MenuState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public MenuState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadMenu(callback: null);
        }

        public void Exit()
        {
            _sceneLoader.UnloadMenu(callback: null);
        }
    }
}
