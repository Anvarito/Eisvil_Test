using UnityEngine;

namespace Infrastructure.Services.ClosestEnemy
{
    public interface IClosestEnemySearcher
    {
       public Transform GetClosestEnemyTransform(Transform originPoint);
    }
}