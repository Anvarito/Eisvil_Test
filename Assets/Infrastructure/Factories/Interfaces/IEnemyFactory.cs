using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Factories.Interfaces
{
    public interface IEnemyFactory : IFactory
    {
        UnityAction OnAllEnemyDead { get; set; }
        void SpawnEnemy(Transform playerTransform);
    }
}
