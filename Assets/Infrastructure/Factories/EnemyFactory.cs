using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Enemy;
using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.EnemyConfigs;
using Infrastructure.Services.TimerServices;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Weapon;

namespace Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory, IEnemyListHolder
    {
        private List<EnemyBrain> _enemies = new List<EnemyBrain>();
        public List<EnemyBrain> Enemies => _enemies;
        public UnityAction<Transform, float> OnEnemyDead { get; set; }

        private readonly SpawnEnemyArea _spawnEnemyArea;
        private readonly IPlayerPosition _playerPosition;
        private readonly IStartTimerService _startTimerService;
        private readonly ICurrentLevelConfig _currentLevelConfig;
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetLoader _assetLoader;

        public EnemyFactory(
            ICurrentLevelConfig currentLevelConfig,
            IStaticDataService staticDataService,
            IAssetLoader assetLoader,
            SpawnEnemyArea spawnEnemyArea,
            IPlayerPosition playerPosition,
            IStartTimerService startTimerService
        )
        {
            _assetLoader = assetLoader;
            _currentLevelConfig = currentLevelConfig;
            _staticDataService = staticDataService;
            _spawnEnemyArea = spawnEnemyArea;
            _playerPosition = playerPosition;
            _startTimerService = startTimerService;
        }

        public async UniTask WarmUp()
        {
        }


        public void SpawnEnemy(Transform playerTransform)
        {
            SpawnEnemiesOfType(EEnemyType.Ground, _currentLevelConfig.CurrentLevelConfig.GroundEnemyCount,
                AssetPaths.EnemyGroundPrefab);
            SpawnEnemiesOfType(EEnemyType.Explosion, _currentLevelConfig.CurrentLevelConfig.ExplosionEnemyCount,
                AssetPaths.ExplosionEnemyPrefab);
            SpawnEnemiesOfType(EEnemyType.Fly, _currentLevelConfig.CurrentLevelConfig.FlyEnemyCount,
                AssetPaths.FlyEnemyPrefab);
        }

        private void SpawnEnemiesOfType(EEnemyType enemyType, int count, string prefabPath)
        {
            for (int i = 0; i < count; i++)
            {
                EnemyView enemyView = CreateView(prefabPath, enemyType);
                EnemyData enemyStaticData = _staticDataService.Enemies.GetValueOrDefault(enemyType);
                IEnemyMoveController moveController = CreateMoveController(enemyType, enemyStaticData, enemyView);
                IHealth health = new Health(enemyStaticData.HitPoints);
                enemyView.GetComponent<DamageRecivier>().OnApplyDamage = (damage) => { health.TakeDamage(damage); };
                EnemyShoot enemyShoot = new EnemyShoot(enemyView.GetComponentInChildren<BaseWeapon>());
                EnemyBrain enemyBrain = enemyView.GetComponent<EnemyBrain>();
                enemyBrain.Init(_playerPosition, enemyStaticData, moveController, health, enemyShoot, _startTimerService);
                enemyBrain.OnDead += AnotherDead;
                _enemies.Add(enemyBrain);
            }
        }

        private void AnotherDead(EnemyBrain concreteEnemy, float killPoints)
        {
            concreteEnemy.OnDead -= AnotherDead;
            _enemies.Remove(concreteEnemy);
            OnEnemyDead?.Invoke(concreteEnemy.transform,killPoints);
            Object.Destroy(concreteEnemy.gameObject);
        }

        private IEnemyMoveController CreateMoveController(EEnemyType enemyType, EnemyData enemyStaticData,
            EnemyView enemyView)
        {
            IEnemyMoveController moveController;

            if (enemyType == EEnemyType.Fly)
            {
                moveController = new FlyEnemyMoveController(enemyStaticData, enemyView.transform);
            }
            else
            {
                moveController = new GroundEnemyMoveController(enemyView.GetComponent<NavMeshAgent>(), enemyStaticData);
            }

            return moveController;
        }

        private EnemyView CreateView(string prefabPath, EEnemyType enemyType)
        {
            EnemyView enemy = _assetLoader.Instantiate<EnemyView>(prefabPath);
            enemy.transform.position = _spawnEnemyArea.GetSpawnPoint();
            enemy.transform.rotation = Quaternion.Euler(0, Random.rotation.eulerAngles.y, 0);
            enemy.transform.name = "Enemy " + enemyType + " " + Random.Range(0, 999);
            return enemy;
        }

        public void CleanUp()
        {
            _enemies.Clear();
            _enemies = null;
        }
    }
}