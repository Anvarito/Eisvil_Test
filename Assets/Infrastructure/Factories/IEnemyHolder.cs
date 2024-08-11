using System.Collections.Generic;
using Enemy;

namespace Infrastructure.Factories
{
    public interface IEnemyHolder
    {
        public Dictionary<IHealth, EnemyView> Enemies { get; }
    }
}