using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Enemy;
using Infrastructure.Constants;
using Infrastructure.Extras;
using Infrastructure.Services.Assets;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.EnemyConfigs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory, IEnemyListHolder
    {
        private ReactiveList<EnemyElement> _enemies = new ReactiveList<EnemyElement>();

        public ReactiveList<EnemyElement> Enemies
        {
            get => _enemies;
            private set => _enemies = value;
        }
        
        public UnityAction OnAllEnemyDead { get; set; }

        private float _spawnDelay;
        private readonly SpawnEnemyArea _spawnEnemyArea;
        private readonly ICurrentLevelConfig _currentLevelConfig;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetLoader _assetLoader;

        public EnemyFactory(
            ICurrentLevelConfig currentLevelConfig,
            IStaticDataService staticDataService,
            IAssetLoader assetLoader,
            SpawnEnemyArea spawnEnemyArea
        )
        {
            _assetLoader = assetLoader;
            _currentLevelConfig = currentLevelConfig;
            _staticDataService = staticDataService;
            _spawnEnemyArea = spawnEnemyArea;
        }

        public async UniTask WarmUp()
        {
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
                EnemyView enemy = CreateEnemyView(prefabPath, enemyType);

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

                EnemyElement enemyElement = new EnemyElement(health, enemy, enemyStaticData.KillPoints);
                _enemies.Add(enemyElement);
            }
        }

        private EnemyView CreateEnemyView(string prefabPath, EEnemyType enemyType)
        {
            EnemyView enemy = _assetLoader.Instantiate<EnemyView>(prefabPath);
            enemy.transform.position = _spawnEnemyArea.GetSpawnPoint();
            enemy.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
            enemy.transform.name = "Enemy " + enemyType + " " + Random.Range(0, 999);
            return enemy;
        }

        private void EnemyDead(IHealth health)
        {
            health.OnDead -= EnemyDead;
            var enemyToRemove = _enemies.FirstOrDefault(x => x.Health == health);
            Object.Destroy(enemyToRemove.View.gameObject);
            _enemies.Remove(enemyToRemove);
                

            if (IsAllEnemyDead())
                OnAllEnemyDead?.Invoke();
        }

        private bool IsAllEnemyDead()
        {
            return !_enemies.Any();
        }

        public void CleanUp()
        {
            _enemies.Clear();
            _enemies = null;
        }
    }
}