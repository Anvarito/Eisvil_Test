using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.PointGoal;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;
using Player;

namespace Infrastructure.States.InGameStates
{
    public class GameLoop: IState
    {
        private IPointScoreService _pointScoreService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ExitTrigger _exitTrigger;
        private readonly PlayerController _playerController;
        private GameStateMachine _gameStateMachine;


        public GameLoop(
            GameStateMachine gameStateMachine, 
            IPointScoreService pointScoreService, 
            IEnemyFactory enemyFactory,
            ExitTrigger exitTrigger,
            PlayerController playerController)
        {
            _gameStateMachine = gameStateMachine;
            _pointScoreService = pointScoreService;
            _enemyFactory = enemyFactory;
            _exitTrigger = exitTrigger;
            _playerController = playerController;

            _playerController.OnDead += OnPlayerDead;
            _enemyFactory.OnAllEnemyDead += AllEnemyDead;
            _exitTrigger.OnPlayerReachExit += PlayerReachExit;
        }

        private void OnPlayerDead()
        {
            _playerController.OnDead -= OnPlayerDead;
            _gameStateMachine.Enter<GameLoose>();
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
            _playerController.OnDead -= OnPlayerDead;
            _enemyFactory.OnAllEnemyDead -= AllEnemyDead;
            _exitTrigger.OnPlayerReachExit -= PlayerReachExit;
        }

        public void Enter()
        {
        }

        
    }
}