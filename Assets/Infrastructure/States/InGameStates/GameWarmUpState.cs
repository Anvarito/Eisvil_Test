using Infrastructure.Services.PointGoal;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameWarmUpState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPointGoalService _pointGoalService;

        public GameWarmUpState(
            GameStateMachine gameStateMachine,
            IPointGoalService pointGoalService
            )
        {
            _pointGoalService = pointGoalService;
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            _pointGoalService.WarmUp();
            _gameStateMachine.Enter<GameLoop>();
        }
    }
}