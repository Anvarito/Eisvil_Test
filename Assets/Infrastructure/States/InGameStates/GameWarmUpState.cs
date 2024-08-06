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

        public GameWarmUpState(
            GameStateMachine gameStateMachine,
            IPointScoreService pointScoreService,
            IEnemyFactory enemyFactory,
            IStaticDataService staticDataService
            )
        {
            _pointScoreService = pointScoreService;
            _enemyFactory = enemyFactory;
            _staticDataService = staticDataService;
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
            _gameStateMachine.Enter<GameLoop>();
        }

        private void CreateEnemy()
        {
            _enemyFactory.WarmUp();
            _enemyFactory.SpawnEnemy();
        }
    }
}