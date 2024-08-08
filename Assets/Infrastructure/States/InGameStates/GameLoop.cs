using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.Logging;
using Infrastructure.Services.PointGoal;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameLoop: IState
    {
        private IPointScoreService _pointScoreService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ExitTrigger _exitTrigger;
        private GameStateMachine _gameStateMachine;


        public GameLoop(
            GameStateMachine gameStateMachine, 
            IPointScoreService pointScoreService, 
            IEnemyFactory enemyFactory,
            ExitTrigger exitTrigger)
        {
            _gameStateMachine = gameStateMachine;
            _pointScoreService = pointScoreService;
            _enemyFactory = enemyFactory;
            _exitTrigger = exitTrigger;

            _enemyFactory.OnAllEnemyDead += AllEnemyDead;
            _exitTrigger.OnPlayerReachExit += PlayerReachExit;
        }

        private void AllEnemyDead()
        {
            _exitTrigger.gameObject.SetActive(true);
        }
        private void PlayerReachExit()
        {
            _gameStateMachine.Enter<GameVictory>();
        }

        public void Exit()
        {
            _pointScoreService.CleanUp();
            _enemyFactory.OnAllEnemyDead -= AllEnemyDead;
            _exitTrigger.OnPlayerReachExit -= PlayerReachExit;
        }

        public void Enter()
        {
        }

        
    }
}