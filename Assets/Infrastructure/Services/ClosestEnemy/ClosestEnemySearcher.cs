using Enemy;
using Infrastructure.Factories;
using ModestTree;
using UnityEngine;

namespace Infrastructure.Services.ClosestEnemy
{
    public class ClosestEnemySearcher : IClosestEnemySearcher
    {
        private EnemyView _nearestTarget;
        private float _closestDistance = float.MaxValue;
        private readonly IEnemyHolder _enemyHolder;
        
        public ClosestEnemySearcher(IEnemyHolder enemyHolder)
        {
            _enemyHolder = enemyHolder;
        }

        public Transform GetClosestEnemyTransform(Transform originPoint)
        {
            if (_enemyHolder.Enemies.IsEmpty())
                return null;
            
            // for(int i =0; i < _enemyHolder.Enemies.Count; i ++)
            // {
            //     float distance = Vector3.Distance(_enemyHolder.Enemies[i].transform.position, originPoint.position);
            //
            //     if (distance < _closestDistance)
            //     {
            //         _closestDistance = distance;
            //         _nearestTarget = _enemyHolder.Enemies[i];
            //     }
            // }

            foreach (var enemy in _enemyHolder.Enemies)
            {
                if(enemy.Value == null) 
                    continue;
                
                float distance = Vector3.Distance(enemy.Value.transform.position, originPoint.position);

                if (distance < _closestDistance)
                {
                    _closestDistance = distance;
                    _nearestTarget = enemy.Value;
                }
            }

            _closestDistance = float.MaxValue;
            return _nearestTarget.transform;
        }
    }
}