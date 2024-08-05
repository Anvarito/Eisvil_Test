using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticData;
using ModestTree;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private float _spawnDelay;
        private List<SpawnEnemyArea> _spawnEnemyAreas = new List<SpawnEnemyArea>();
        private CancellationTokenSource _cancellationTokenSource;
        private ICurrentLevelConfig _currentLevelConfig;
        private Enemy.Factory _enemyFactory;
        private ILoggingService _loggingService;

        public EnemyFactory(ICurrentLevelConfig currentLevelConfig, Enemy.Factory enemyFactory, ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _enemyFactory = enemyFactory;
            _currentLevelConfig = currentLevelConfig;
        }

        public async UniTask WarmUp()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _spawnEnemyAreas = Object.FindObjectsOfType<SpawnEnemyArea>().ToList();
        }

        public async UniTask SpawnEnemy()
        {
            foreach (var area in _spawnEnemyAreas)
            {
                await UniTask.WaitForSeconds(_currentLevelConfig.CurrentLevelConfig.SpawnDelay,false,PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                var enemy = _enemyFactory.Create();
                enemy.transform.parent = area.transform;
                enemy.transform.position = area.GetSpawnPoint();
                enemy.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
            }

            if (_spawnEnemyAreas.IsEmpty())
            {
                _loggingService.LogMessage("Not have spawn cargo areas!");
                _cancellationTokenSource.Cancel();
            }
            
            await SpawnEnemy();
        }

        public void CleanUp()
        {
            _cancellationTokenSource.Cancel();
            _spawnEnemyAreas = null;
        }
    }
}
