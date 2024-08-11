using System;
using Enemy;
using Infrastructure.Services.StaticData.EnemyConfigs;
using Player;
using UnityEngine;

public class ExplosionEnemy : EnemyView
{
    
    public override void Init(IHealth health, Transform target, EnemyData enemyStaticData,
        IEnemyMoveController enemyMoveController)
    {
        _health = health;
        _playerTarget = target;
        _enemyStaticData = enemyStaticData;
        _enemyMoveController = enemyMoveController;

        _damageRecivier.OnApplyDamage += TakeDamage;
    }

    protected override void Update()
    {
        if (IsPlayerDetect())
        {
            MoveBehaviour();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        print("COL");
        if (other.transform.TryGetComponent(out PlayerView playerView))
        {
            playerView.GetComponent<DamageRecivier>().ApplyDamage(_enemyStaticData.HitPoints);
            _damageRecivier.ApplyDamage(int.MaxValue);
        }
    }
}