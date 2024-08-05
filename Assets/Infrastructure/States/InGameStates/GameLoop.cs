using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.PointGoal;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameLoop: IState
    {
        private IEnemyFactory _enemyFactory;
        private IPointGoalService _pointGoalService;
        private GameStateMachine _gameStateMachine;


        public GameLoop(GameStateMachine gameStateMachine,IEnemyFactory enemyFactory, IPointGoalService pointGoalService)
        {
            _gameStateMachine = gameStateMachine;
            _pointGoalService = pointGoalService;
            _enemyFactory = enemyFactory;
        }
       

        public void Exit()
        {
            _pointGoalService.OnPointsGoal -= OnPointsGoal;
            _pointGoalService.CleanUp();
            _enemyFactory.CleanUp();
        }

        public void Enter()
        {
            _pointGoalService.OnPointsGoal += OnPointsGoal;
            _enemyFactory.SpawnCargo();
        }

        private void OnPointsGoal()
        {
            _gameStateMachine.Enter<GameVictory>();
        }
    }
}