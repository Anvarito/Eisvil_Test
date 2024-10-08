using System.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Services.NavMeshBuild;
using Infrastructure.Services.PointGoal;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.TimerServices;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;
using Player;
using UnityEngine;

namespace Infrastructure.States.InGameStates
{
    public class GameWarmUpState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPointScoreService _pointScoreService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ExitTrigger _exitTrigger;
        private readonly IStartTimerService _startTimerService;
        private readonly INavMeshRebuildService _navMeshRebuildService;

        public GameWarmUpState(
            GameStateMachine gameStateMachine,
            IPointScoreService pointScoreService,
            IEnemyFactory enemyFactory,
            ExitTrigger exitTrigger,
            IStartTimerService startTimerService,
            INavMeshRebuildService navMeshRebuildService
            )
        {
            _pointScoreService = pointScoreService;
            _enemyFactory = enemyFactory;
            _exitTrigger = exitTrigger;
            _startTimerService = startTimerService;
            _navMeshRebuildService = navMeshRebuildService;
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }

        public async void Enter()
        {
            _navMeshRebuildService.RebuildNavMesh();
            _startTimerService.Launch();
            CreateEnemy();
            _pointScoreService.WarmUp();
            _exitTrigger.gameObject.SetActive(false);
            _gameStateMachine.Enter<GameLoop>();
        }

        private void CreateEnemy()
        {
            _enemyFactory.SpawnEnemy(Object.FindObjectOfType<PlayerView>().transform);
        }
    }
}