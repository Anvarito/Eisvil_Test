using Infrastructure.Services.StaticData.EnemyConfigs;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemy.EnemyMove
{
    public class GroundMoveController : IEnemyMoveController
    {
        private NavMeshAgent _navMeshAgent;
        private readonly EnemyData _enemyStaticData;

        public GroundMoveController(NavMeshAgent navMeshAgent, EnemyData enemyStaticData)
        {
            _navMeshAgent = navMeshAgent;
            _enemyStaticData = enemyStaticData;
        }
        
        public void SetMoveTarget(Transform target)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(target.position);
        }
        
        public void SetStop()
        {
            _navMeshAgent.isStopped = true;
        }

        public void RotateTo(Vector3 targetPosition)
        {
            var transform = _navMeshAgent.transform;
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0;
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            float alpha = _enemyStaticData.AngularSpeed * Time.deltaTime;
            Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, alpha);
        
            transform.rotation = newRotation;
        }
    }
}