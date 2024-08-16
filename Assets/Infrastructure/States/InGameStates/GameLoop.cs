using Infrastructure.Factories;
using Infrastructure.Services.PointGoal;
using Infrastructure.Services.TimerServices;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;
using Player;
using UnityEngine;
using Zenject;

namespace Infrastructure.States.InGameStates
{
    public class GameLoop : IState
    {
        private IPointScoreService _pointScoreService;
        private readonly IEnemyListHolder _enemyListHolder;
        private readonly ExitTrigger _exitTrigger;
        private readonly MoveBlocker _moveBlocker;
        private readonly Health _playerHealth;
        private readonly IStartTimerService _startTimerService;
        private GameStateMachine _gameStateMachine;


        public GameLoop(
            GameStateMachine gameStateMachine,
            IPointScoreService pointScoreService,
            IEnemyListHolder enemyListHolder,
            ExitTrigger exitTrigger,
            MoveBlocker moveBlocker,
            [Inject(Id = "Player health")] Health health,
            IStartTimerService startTimerService)
        {
            _gameStateMachine = gameStateMachine;
            _pointScoreService = pointScoreService;
            _enemyListHolder = enemyListHolder;
            _exitTrigger = exitTrigger;
            _moveBlocker = moveBlocker;
            _playerHealth = health;
            _startTimerService = startTimerService;
        }

        private void StartTimerOut()
        {
            _startTimerService.OnTimerOut -= StartTimerOut;
            _moveBlocker.EnableControl();
        }

        private void OnPlayerDead(IHealth health)
        {
            health.OnDead -= OnPlayerDead;
            _gameStateMachine.Enter<GameLoose>();
        }

        private void EnemyDead(Transform enemyTransform, float killPoints)
        {
            if (IsAllEnemyDead())
                _exitTrigger.gameObject.SetActive(true);
        }

        private bool IsAllEnemyDead()
        {
            return _enemyListHolder.Enemies.Count == 0;
        }

        private void PlayerReachExit()
        {
            _gameStateMachine.Enter<GameVictory>();
        }

        public void Exit()
        {
            _pointScoreService.CleanUp();
            _moveBlocker.DisableControl();

            _playerHealth.OnDead -= OnPlayerDead;
            _enemyListHolder.OnEnemyDead -= EnemyDead;
            _exitTrigger.OnPlayerReachExit -= PlayerReachExit;
            _startTimerService.OnTimerOut -= StartTimerOut;
        }


        public void Enter()
        {
            _playerHealth.OnDead += OnPlayerDead;
            _enemyListHolder.OnEnemyDead += EnemyDead;
            _exitTrigger.OnPlayerReachExit += PlayerReachExit;
            _startTimerService.OnTimerOut += StartTimerOut;
        }
    }
}