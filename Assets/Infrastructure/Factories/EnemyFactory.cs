using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Constants;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class EnemyHolderFactory : IEnemyFactory, IEnemyHolder
    {
        public List<Enemy> Enemies { get; private set; }
        
        private float _spawnDelay;
        private SpawnEnemyArea _spawnEnemyArea;
        private readonly ICurrentLevelConfig _currentLevelConfig;
        private readonly IStaticDataService _staticDataService;
        private readonly ILoggingService _loggingService;
        private readonly IAssetLoader _assetLoader;

        public EnemyHolderFactory(
            ICurrentLevelConfig currentLevelConfig,
            IStaticDataService staticDataService,
            ILoggingService loggingService,
            IAssetLoader assetLoader)
        {
            _loggingService = loggingService;
            _assetLoader = assetLoader;
            _currentLevelConfig = currentLevelConfig;
            _staticDataService = staticDataService;
        }

        public async UniTask WarmUp()
        {
            Enemies = new List<Enemy>();
            _spawnEnemyArea = Object.FindObjectOfType<SpawnEnemyArea>();
        }

        public void SpawnEnemy()
        {
            for (int i = 0; i < _currentLevelConfig.CurrentLevelConfig.GroundEnemyCount; i++)
            {
                Enemy enemy = _assetLoader.Instantiate<Enemy>(AssetPaths.EnemyGroundPrefab);
                enemy.transform.parent = _spawnEnemyArea.transform;
                enemy.transform.position = _spawnEnemyArea.GetSpawnPoint();
                enemy.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
                EnemyData enemyStaticData = _staticDataService.Enemies.GetValueOrDefault(EEnemyType.Ground);
                IHitPoints hitPointsHolder = new HitPointsHolder(enemy.GetComponent<DamageRecivier>(), enemyStaticData.HitPoints);
                enemy.Init(hitPointsHolder);
                enemy.OnDead += EnemyDead;
                Enemies.Add(enemy);
            }
        }

        private void EnemyDead(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }

        public void CleanUp()
        {
            Enemies.Clear();
            Enemies = null;
            _spawnEnemyArea = null;
        }
    }
}