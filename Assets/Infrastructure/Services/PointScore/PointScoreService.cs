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

        private int points = 0;
        
        public PointScoreService(IEnemyListHolder enemyListHolder, PointsScreen pointsScreen, ILevelProgressService levelProgressService)
        {
            _enemyListHolder = enemyListHolder;
            _pointsScreen = pointsScreen;
            _levelProgressService = levelProgressService;
            _enemyListHolder.Enemies.OnElementRemove += EnemyDestroy;
        }

        private void EnemyDestroy(EnemyElement enemy)
        {
            points += enemy.KillPoints;
            _pointsScreen.SetPoints(points);
        }


        public void WarmUp()
        {
            points = _levelProgressService.LoadPoints();
            _pointsScreen.SetPoints(points);
        }


        public void CleanUp()
        {
            _levelProgressService.SavePoints(points);
            _enemyListHolder.Enemies.OnElementRemove -= EnemyDestroy;
            points = 0;
        }

        
    }
}
