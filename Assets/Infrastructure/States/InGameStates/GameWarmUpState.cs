using System.Threading.Tasks;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.PointGoal;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.TimerServices;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameWarmUpState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPointScoreService _pointScoreService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ExitTrigger _exitTrigger;
        private readonly IStartTimerService _startTimerService;

        public GameWarmUpState(
            GameStateMachine gameStateMachine,
            IPointScoreService pointScoreService,
            IEnemyFactory enemyFactory,
            ExitTrigger exitTrigger,
            IStartTimerService startTimerService
            )
        {
            _pointScoreService = pointScoreService;
            _enemyFactory = enemyFactory;
            _exitTrigger = exitTrigger;
            _startTimerService = startTimerService;
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }

        public async void Enter()
        {
            _startTimerService.Launch();
            CreateEnemy();
            _pointScoreService.WarmUp();
            _exitTrigger.gameObject.SetActive(false);
            _gameStateMachine.Enter<GameLoop>();
        }

        private void CreateEnemy()
        {
            _enemyFactory.WarmUp();
            _enemyFactory.SpawnEnemy();
        }
    }
}