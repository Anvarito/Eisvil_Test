using Infrastructure.Services.StaticData.EnemyConfigs;
using UnityEngine;

namespace Enemy
{
    public class FlyEnemyMoveController : IEnemyMoveController
    {
        private readonly EnemyData _enemyStaticData;
        private readonly Transform _selfTransform;

        public FlyEnemyMoveController(EnemyData enemyStaticData, Transform selfTransform)
        {
            _enemyStaticData = enemyStaticData;
            _selfTransform = selfTransform;
        }
        
        public void SetMoveTarget(Vector3 target)
        {
            Vector3 direction = (target - _selfTransform.position).normalized;
            direction.y = 0;
            _selfTransform.Translate(direction * (_enemyStaticData.MoveSpeed * Time.deltaTime), Space.World);
            RotateTo(target);
        }
        
        public void SetStop()
        {
            _selfTransform.Translate(Vector3.zero);
        }

        public void RotateTo(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - _selfTransform.position).normalized;
            direction.y = 0;
            Quaternion currentRotation = _selfTransform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float alpha = _enemyStaticData.AngularSpeed * Time.deltaTime;
            Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, alpha);
        
            _selfTransform.rotation = newRotation;
        }
    }
}