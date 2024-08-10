using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Constants;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.EnemyConfigs;
using Scripts.Enemy.EnemyMove;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public class EnemyHolderFactory : IEnemyFactory, IEnemyHolder
    {
        public List<Enemy> Enemies { get; private set; }
        public UnityAction OnAllEnemyDead { get; set; }

        private float _spawnDelay;
        private SpawnEnemyArea _spawnEnemyArea;
        private readonly ICurrentLevelConfig _currentLevelConfig;
        private readonly IStaticDataService _staticDataService;
        private readonly ILoggingService _loggingService;
        private readonly IAssetLoader _assetLoader;
        private readonly PlayerView _playerView;

        public EnemyHolderFactory(
            ICurrentLevelConfig currentLevelConfig,
            IStaticDataService staticDataService,
            ILoggingService loggingService,
            IAssetLoader assetLoader,
            PlayerView playerView)
        {
            _loggingService = loggingService;
            _assetLoader = assetLoader;
            _playerView = playerView;
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
                Enemy enemy = CreateEnemyView(AssetPaths.EnemyGroundPrefab);
                
                EnemyData enemyStaticData = _staticDataService.Enemies.GetValueOrDefault(EEnemyType.Ground);
                IEnemyMoveController groundMoveController = new GroundMoveController(enemy.GetComponent<NavMeshAgent>(), enemyStaticData);
                enemy.Init(new HitPointsHolder(enemyStaticData.HitPoints), _playerView.transform, enemyStaticData, groundMoveController);
                enemy.OnDead += EnemyDead;
                Enemies.Add(enemy);
            }
        }

        private Enemy CreateEnemyView(string prefabPath)
        {
            Enemy enemy = _assetLoader.Instantiate<Enemy>(prefabPath);
            enemy.transform.position = _spawnEnemyArea.GetSpawnPoint();
            enemy.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
            enemy.transform.name = "Ground enemy " + Random.Range(0, 999);
            return enemy;
        }


        private void EnemyDead(Enemy enemy)
        {
            enemy.OnDead -= EnemyDead;
            Enemies.Remove(enemy);
            
            if (IsAllEnemyDead())
                OnAllEnemyDead?.Invoke();
        }

        private bool IsAllEnemyDead()
        {
            return Enemies.Count == 0;
        }

        public void CleanUp()
        {
            Enemies.Clear();
            Enemies = null;
            _spawnEnemyArea = null;
        }
    }
}