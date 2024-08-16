using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Factories
{
    public interface IEnemyListHolder
    {
        UnityAction<Transform, float> OnEnemyDead { get; set; }
        public List<EnemyBrain> Enemies { get; }
    }
}