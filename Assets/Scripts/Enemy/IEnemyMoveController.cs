using UnityEngine;

namespace Enemy
{
    public interface IEnemyMoveController
    {
        void SetMoveTarget(Vector3 target);
        void SetStop();
        void RotateTo(Vector3 targetPosition);
    }
}