using UnityEngine;

namespace Infrastructure.Services.ClosestEnemy
{
    public interface ISearchClosestEnemy
    {
       public Transform GetClosestEnemyTransform();
    }
}