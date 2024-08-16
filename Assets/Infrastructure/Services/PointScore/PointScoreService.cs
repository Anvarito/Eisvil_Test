using Infrastructure.Factories;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.PointGoal;
using UnityEngine;

namespace Infrastructure.Services.PointScore
{
    public class PointScoreService : IPointScoreService
    {
        private readonly IEnemyListHolder _enemyListHolder;
        private readonly PointsScreen _pointsScreen;
        private readonly ILevelProgressService _levelProgressService;

        private int _points = 0;
        
        public PointScoreService(IEnemyListHolder enemyListHolder, PointsScreen pointsScreen, ILevelProgressService levelProgressService)
        {
            _enemyListHolder = enemyListHolder;
            _pointsScreen = pointsScreen;
            _levelProgressService = levelProgressService;
            _enemyListHolder.OnEnemyDead += EnemyDestroy;
        }

        private void EnemyDestroy(Transform enemyTransform, float killPoints)
        {
            _points += (int)killPoints;
            _pointsScreen.SetPoints(_points);
        }

        public void WarmUp()
        {
            _points = _levelProgressService.LoadPoints();
            _pointsScreen.SetPoints(_points);
        }


        public void CleanUp()
        {
            _levelProgressService.SavePoints(_points);
            _enemyListHolder.OnEnemyDead -= EnemyDestroy;
            _points = 0;
        }

        
    }
}
