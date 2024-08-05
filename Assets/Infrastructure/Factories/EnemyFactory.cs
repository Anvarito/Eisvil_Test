using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Logging;
using ModestTree;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private float _spawnDelay;
        private List<SpawnEnemyArea> _spawnCargoAreas = new List<SpawnEnemyArea>();
        private CancellationTokenSource _cancellationTokenSource;
        private ICurrentLevelConfig _currentLevelConfig;
        private Enemy.Factory _cargoFactory;
        private ILoggingService _loggingService;

        public EnemyFactory(ICurrentLevelConfig currentLevelConfig, Enemy.Factory cargoFactory, ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _cargoFactory = cargoFactory;
            _currentLevelConfig = currentLevelConfig;
        }

        public async UniTask WarmUp()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _spawnCargoAreas = Object.FindObjectsOfType<SpawnEnemyArea>().ToList();
        }

        public async UniTask SpawnCargo()
        {
            foreach (var area in _spawnCargoAreas)
            {
                await UniTask.WaitForSeconds(_currentLevelConfig.CurrentLevelConfig.SpawnDelay,false,PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                var cargo = _cargoFactory.Create();
                cargo.transform.parent = area.transform;
                cargo.transform.position = area.GetSpawnPoint();
                cargo.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
            }

            if (_spawnCargoAreas.IsEmpty())
            {
                _loggingService.LogMessage("Not have spawn cargo areas!");
                _cancellationTokenSource.Cancel();
            }
            
            await SpawnCargo();
        }

        public void CleanUp()
        {
            _cancellationTokenSource.Cancel();
            _spawnCargoAreas = null;
        }
    }
}