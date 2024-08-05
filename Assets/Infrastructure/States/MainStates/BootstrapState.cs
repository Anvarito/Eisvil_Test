
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.MainStates
{
    public class BootstrapState : IState
    {
        private readonly MainStateMachine _stateMachine;

        public BootstrapState(MainStateMachine mainStateMachine)
        {
            _stateMachine = mainStateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadGameState>();
        }

        public void Exit()
        {
        }
    }
}