using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy;
using Infrastructure.Constants;
using Infrastructure.Factories.Interfaces;
using Infrastructure.Services.Assets;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.EnemyConfigs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory, IEnemyHolder
    {
        public Dictionary<IHealth, EnemyView> Enemies { get; private set; }
        public UnityAction OnAllEnemyDead { get; set; }

        private float _spawnDelay;
        private SpawnEnemyArea _spawnEnemyArea;
        private readonly ICurrentLevelConfig _currentLevelConfig;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetLoader _assetLoader;

        public EnemyFactory(
            ICurrentLevelConfig currentLevelConfig,
            IStaticDataService staticDataService,
            IAssetLoader assetLoader
        )
        {
            _assetLoader = assetLoader;
            _currentLevelConfig = currentLevelConfig;
            _staticDataService = staticDataService;
        }

        public async UniTask WarmUp()
        {
            Enemies = new Dictionary<IHealth, EnemyView>();
            _spawnEnemyArea = Object.FindObjectOfType<SpawnEnemyArea>();
        }


        public void SpawnEnemy(Transform playerTransform)
        {
            SpawnEnemiesOfType(EEnemyType.Ground, _currentLevelConfig.CurrentLevelConfig.GroundEnemyCount, playerTransform, AssetPaths.EnemyGroundPrefab);
            SpawnEnemiesOfType(EEnemyType.Explosion, _currentLevelConfig.CurrentLevelConfig.ExplosionEnemyCount, playerTransform, AssetPaths.ExplosionEnemyPrefab);
            SpawnEnemiesOfType(EEnemyType.Fly, _currentLevelConfig.CurrentLevelConfig.FlyEnemyCount, playerTransform, AssetPaths.FlyEnemyPrefab);
        }

        private void SpawnEnemiesOfType(EEnemyType enemyType, int count, Transform playerTransform, string prefabPath)
        {
            for (int i = 0; i < count; i++)
            {
                EnemyView enemy = CreateEnemyView(prefabPath);

                EnemyData enemyStaticData = _staticDataService.Enemies.GetValueOrDefault(enemyType);
                IEnemyMoveController moveController;

                if (enemyType == EEnemyType.Fly)
                {
                    moveController = new FlyEnemyMoveController(enemyStaticData, enemy.transform);
                }
                else
                {
                    moveController = new GroundEnemyMoveController(enemy.GetComponent<NavMeshAgent>(), enemyStaticData);
                }

                IHealth health = new Health(enemyStaticData.HitPoints);
                health.OnDead += EnemyDead;
                enemy.Init(health, playerTransform, enemyStaticData, moveController);
                Enemies.Add(health, enemy);
            }
        }

        private EnemyView CreateEnemyView(string prefabPath)
        {
            EnemyView enemy = _assetLoader.Instantiate<EnemyView>(prefabPath);
            enemy.transform.position = _spawnEnemyArea.GetSpawnPoint();
            enemy.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
            enemy.transform.name = "Enemy " + Random.Range(0, 999);
            return enemy;
        }

        private void EnemyDead(IHealth health)
        {
            health.OnDead -= EnemyDead;
            Object.Destroy(Enemies[health].gameObject);
            Enemies.Remove(health);

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