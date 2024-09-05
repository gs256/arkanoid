namespace Arkanoid.Base.GameStates
{
    public class GameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public GameState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadGame(OnGameLoaded);
        }

        public void Exit()
        {
            _sceneLoader.UnloadGame(callback: null);
        }

        private void OnGameLoaded()
        {
        }
    }
}
