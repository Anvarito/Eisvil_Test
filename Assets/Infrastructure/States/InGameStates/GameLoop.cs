using Infrastructure.Services.PointGoal;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameLoop: IState
    {
        private IPointScoreService _pointScoreService;
        private GameStateMachine _gameStateMachine;


        public GameLoop(GameStateMachine gameStateMachine, IPointScoreService pointScoreService)
        {
            _gameStateMachine = gameStateMachine;
            _pointScoreService = pointScoreService;
        }
       

        public void Exit()
        {
            _pointScoreService.CleanUp();
        }

        public void Enter()
        {
        }

        private void OnPointsGoal()
        {
            _gameStateMachine.Enter<GameVictory>();
        }
    }
}