using Infrastructure.Factories;
using ModestTree;
using UnityEngine;

namespace Infrastructure.Services.ClosestEnemy
{
    public class SearchClosestEnemy : IService, ISearchClosestEnemy
    {
        private Enemy _nearestTarget;
        private float _closestDistance = float.MaxValue;
        private readonly IEnemyHolder _enemyHolder;
        private readonly IPlayerView _playerView;
        
        public SearchClosestEnemy(IEnemyHolder enemyHolder, IPlayerView playerView)
        {
            _enemyHolder = enemyHolder;
            _playerView = playerView;
        }

        public Transform GetClosestEnemyTransform()
        {
            if (_enemyHolder.Enemies.IsEmpty())
                return null;
            
            for(int i =0; i < _enemyHolder.Enemies.Count; i ++)
            {
                float distance = Vector3.Distance(_enemyHolder.Enemies[i].transform.position, _playerView.Transform.position);

                if (distance < _closestDistance)
                {
                    _closestDistance = distance;
                    _nearestTarget = _enemyHolder.Enemies[i];
                }
            }

            _closestDistance = float.MaxValue;
            return _nearestTarget.transform;
        }


        public void CleanUp()
        {
            
        }
    }
}