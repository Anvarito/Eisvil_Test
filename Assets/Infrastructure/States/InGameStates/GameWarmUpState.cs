using Infrastructure.Factories;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.PointGoal;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameWarmUpState : IState
    {
        private readonly IEnemyFactory _enemyFactory;
        private GameStateMachine _gameStateMachine;
        private IPointGoalService _pointGoalService;

        public GameWarmUpState(
            GameStateMachine gameStateMachine,
            IEnemyFactory enemyFactory,
            IPointGoalService pointGoalService
            )
        {
            _pointGoalService = pointGoalService;
            _gameStateMachine = gameStateMachine;
            _enemyFactory = enemyFactory;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            _pointGoalService.WarmUp();
            _enemyFactory.WarmUp();
            
            _gameStateMachine.Enter<GameLoop>();
        }
    }
}