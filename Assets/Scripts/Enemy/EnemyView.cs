using Infrastructure.Services.StaticData.EnemyConfigs;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyView : MonoBehaviour
    {
        [SerializeField] protected EnemyShoot _enemyShoot;
        [SerializeField] protected DamageRecivier _damageRecivier;

        protected IHealth _health;
        protected Transform _playerTarget;
        protected EnemyData _enemyStaticData;
        protected IEnemyMoveController _enemyMoveController;
        
        private float _moveDistance;
        private bool _isPlayerDetected;

        public virtual void Init(IHealth health, Transform target, EnemyData enemyStaticData,
            IEnemyMoveController enemyMoveController)
        {
            _health = health;
            _playerTarget = target;
            _enemyStaticData = enemyStaticData;
            _enemyMoveController = enemyMoveController;

            _enemyShoot.Init(_enemyStaticData.FireRate);
            _damageRecivier.OnApplyDamage += TakeDamage;
        }

        protected void TakeDamage(int damageAmount)
        {
            _health.TakeDamage(damageAmount);
        }

        private void OnDestroy()
        {
            _damageRecivier.OnApplyDamage -= TakeDamage;
        }

        protected virtual void Update()
        {
            if (IsPlayerDetect())
            {
                if (PlayerInFireRange())
                {
                    ShootBehaviour();
                }
                else
                {
                    MoveBehaviour();
                }
            }
        }

        protected bool PlayerInFireRange()
        {
            var shootDistance = Vector3.Distance(_playerTarget.position, transform.position);
            return shootDistance <= _enemyStaticData.ShootDistance;
        }

        protected bool IsPlayerDetect()
        {
            if (_isPlayerDetected)
                return true;

            _moveDistance = Vector3.Distance(_playerTarget.position, transform.position);
            _isPlayerDetected = _moveDistance <= _enemyStaticData.DetectRadius;

            return _isPlayerDetected;
        }

        protected void MoveBehaviour()
        {
            _enemyMoveController.SetMoveTarget(_playerTarget);
        }

        protected void ShootBehaviour()
        {
            _enemyMoveController.SetStop();
            _enemyMoveController.RotateTo(_playerTarget.position);
            
            _enemyShoot.Shoot();
        }
    }
}