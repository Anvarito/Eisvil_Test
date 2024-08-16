using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public interface IEnemyFactory : IFactory
    {
        void SpawnEnemy(Transform playerTransform);
    }
}
