using UnityEngine;

namespace Enemy
{
    public interface IEnemyMoveController
    {
        void SetMoveTarget(Transform target);
        void SetStop();
        void RotateTo(Vector3 targetPosition);
    }
}