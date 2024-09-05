namespace Arkanoid.Base.GameStates
{
    public class InitializationState : IGameState
    {
        private readonly GameStateMachine _stateMachine;

        public InitializationState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
        }
    }
}
