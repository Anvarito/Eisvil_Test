using Infrastructure.Services.StaticData.EnemyConfigs;
using Infrastructure.Services.TimerServices;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        public UnityAction<EnemyBrain, float> OnDead { get; set; }
        
        private IPlayerPosition _playerPosition;
        private EnemyData _enemyData;
        private IEnemyMoveController _enemyMoveController;
        private IStartTimerService _startTimerService;
        private EnemyShoot _enemyShoot;
        private Transform _playerTarget;
        private IHealth _health;

        private bool _isPlayerDetected;
        private float _moveDistance;
        private bool _isAllowMove;

        public void Init(IPlayerPosition playerPosition,
            EnemyData enemyData,
            IEnemyMoveController enemyMoveController,
            IHealth health,
            EnemyShoot enemyShoot,
            IStartTimerService startTimerService)
        {
            _playerPosition = playerPosition;
            _enemyData = enemyData;
            _enemyMoveController = enemyMoveController;
            _health = health;
            _enemyShoot = enemyShoot;
            _startTimerService = startTimerService;
            _playerTarget = _playerPosition.PlayerTransform;

            _health.CurrentHitPoints.OnValueChanged += HitPointsChange;
            _startTimerService.OnTimerOut += OnTimerOut;
        }
        private void HitPointsChange(int newHitPoints)
        {
            print(gameObject.name + " is hit!");
            if (newHitPoints <= 0)
            {
                _health.CurrentHitPoints.OnValueChanged -= HitPointsChange;
                OnDead?.Invoke(this, _enemyData.KillPoints);
            }
        }


        private void OnTimerOut()
        {
            _startTimerService.OnTimerOut -= OnTimerOut;
            _isAllowMove = true;
        }

        private void Update()
        {
            if (!_isAllowMove)
                return;

            if (IsPlayerDetect())
            {
                if (PlayerInFireRange() && HaveGun())
                {
                    ShootBehaviour();
                }
                else
                {
                    MoveBehaviour();
                }
            }
        }

        private bool HaveGun()
        {
            return _enemyShoot.HaveGun;
        }

        private bool PlayerInFireRange()
        {
            var shootDistance = Vector3.Distance(_playerTarget.position, transform.position);
            return shootDistance <= _enemyData.ShootDistance;
        }

        private bool IsPlayerDetect()
        {
            if (_isPlayerDetected)
                return true;

            _moveDistance = Vector3.Distance(_playerTarget.position, transform.position);
            _isPlayerDetected = _moveDistance <= _enemyData.DetectRadius;

            return _isPlayerDetected;
        }

        private void MoveBehaviour()
        {
            _enemyMoveController.SetMoveTarget(_playerTarget.position);
            _enemyShoot.StopShooting();
        }

        private void ShootBehaviour()
        {
            _enemyMoveController.SetStop();
            _enemyMoveController.RotateTo(_playerTarget.position);

            _enemyShoot.Shoot();
        }
    }
}