using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public interface IEnemyFactory : IFactory
    {
        UnityAction OnAllEnemyDead { get; set; }
        void SpawnEnemy(Transform playerTransform);
    }
}
