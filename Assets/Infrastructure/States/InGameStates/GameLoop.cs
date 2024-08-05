using Infrastructure.Services.PointGoal;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameLoop: IState
    {
        private IPointGoalService _pointGoalService;
        private GameStateMachine _gameStateMachine;


        public GameLoop(GameStateMachine gameStateMachine, IPointGoalService pointGoalService)
        {
            _gameStateMachine = gameStateMachine;
            _pointGoalService = pointGoalService;
        }
       

        public void Exit()
        {
            _pointGoalService.OnPointsGoal -= OnPointsGoal;
            _pointGoalService.CleanUp();
        }

        public void Enter()
        {
            _pointGoalService.OnPointsGoal += OnPointsGoal;
        }

        private void OnPointsGoal()
        {
            _gameStateMachine.Enter<GameVictory>();
        }
    }
}