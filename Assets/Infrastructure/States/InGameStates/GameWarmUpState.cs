using System.Threading.Tasks;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.PointGoal;
using Infrastructure.Services.StaticData;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameWarmUpState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPointScoreService _pointScoreService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ExitTrigger _exitTrigger;

        public GameWarmUpState(
            GameStateMachine gameStateMachine,
            IPointScoreService pointScoreService,
            IEnemyFactory enemyFactory,
            IStaticDataService staticDataService,
            ExitTrigger exitTrigger
            )
        {
            _pointScoreService = pointScoreService;
            _enemyFactory = enemyFactory;
            _staticDataService = staticDataService;
            _exitTrigger = exitTrigger;
            _gameStateMachine = gameStateMachine;
        }

        public void Exit()
        {
            
        }

        public async void Enter()
        {
            _staticDataService.ForLevel("1");
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