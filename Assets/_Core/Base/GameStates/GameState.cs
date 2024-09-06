
namespace Arkanoid.Base.GameStates
{
    public class GameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private Game _game;

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
            _game.Dispose();
            _sceneLoader.UnloadGame(callback: null);
        }

        private void OnGameLoaded()
        {
            _game = GameContext.Instance.Game;
            _game.LoadGame();
        }
    }
}
