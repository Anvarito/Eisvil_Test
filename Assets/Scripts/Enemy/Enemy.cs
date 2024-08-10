using Infrastructure.Services.StaticData.EnemyConfigs;
using Scripts.Enemy.EnemyMove;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyShoot _enemyShoot;
    [SerializeField] private DamageRecivier _damageRecivier;

    public UnityAction<Enemy> OnDead;

    private IHitPoints _hitPoints;
    private Transform _playerTarget;
    private EnemyData _enemyStaticData;
    private IEnemyMoveController _enemyMoveController;
    private float _moveDistance;
    private bool _isPlayerDetected;

    public void Init(IHitPoints hitPoints, Transform target, EnemyData enemyStaticData,
        IEnemyMoveController enemyMoveController)
    {
        _hitPoints = hitPoints;
        _playerTarget = target;
        _enemyStaticData = enemyStaticData;
        _enemyMoveController = enemyMoveController;

        _enemyShoot.Init(_enemyStaticData.FireRate);
        _damageRecivier.OnApplyDamage += TakeDamage;
    }

    private void TakeDamage(int damageAmount)
    {
        if (_hitPoints.DecreaseHitPoints(damageAmount) <= 0)
        {
            EnemyDead();
        }
    }

    private void EnemyDead()
    {
        OnDead?.Invoke(this);
        Destroy(gameObject);
    }

    private void Update()
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

    private bool PlayerInFireRange()
    {
        var shootDistance = Vector3.Distance(_playerTarget.position, transform.position);
        return shootDistance <= _enemyStaticData.ShootDistance;
    }

    private bool IsPlayerDetect()
    {
        if (_isPlayerDetected)
            return true;

        _moveDistance = Vector3.Distance(_playerTarget.position, transform.position);
        _isPlayerDetected = _moveDistance <= _enemyStaticData.DetectRadius;

        return _isPlayerDetected;
    }

    private void MoveBehaviour()
    {
        _enemyMoveController.SetMoveTarget(_playerTarget);
    }

    private void ShootBehaviour()
    {
        _enemyMoveController.SetStop();
        _enemyMoveController.RotateTo(_playerTarget.position);
        
        _enemyShoot.Shoot();
    }
}