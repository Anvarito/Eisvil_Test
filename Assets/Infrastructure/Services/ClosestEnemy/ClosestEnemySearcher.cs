using Enemy;
using Infrastructure.Factories;
using ModestTree;
using UnityEngine;

namespace Infrastructure.Services.ClosestEnemy
{
    public class ClosestEnemySearcher : IClosestEnemySearcher
    {
        private Transform _nearestTarget;
        private float _closestDistance = float.MaxValue;
        private readonly IEnemyListHolder _enemyListHolder;
        
        public ClosestEnemySearcher(IEnemyListHolder enemyListHolder)
        {
            _enemyListHolder = enemyListHolder;
        }

        public Transform GetClosestEnemyTransform(Transform originPoint)
        {
            if (_enemyListHolder.Enemies.IsEmpty())
                return null;

            foreach (var enemy in _enemyListHolder.Enemies)
            {
                if(enemy == null) 
                    continue;
                
                float distance = Vector3.Distance(enemy.transform.position, originPoint.position);

                if (distance < _closestDistance)
                {
                    _closestDistance = distance;
                    _nearestTarget = enemy.transform;
                }
            }

            _closestDistance = float.MaxValue;
            return _nearestTarget.transform;
        }
    }
}