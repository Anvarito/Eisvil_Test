using UnityEngine;

namespace Scripts.Enemy.EnemyMove
{
    public interface IEnemyMoveController
    {
        void SetMoveTarget(Transform target);
        void SetStop();
        void RotateTo(Vector3 targetPosition);
    }
}