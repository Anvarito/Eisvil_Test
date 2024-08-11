using System.Collections.Generic;
using Enemy;

namespace Infrastructure.Factories
{
    public interface IEnemyHolder
    {
        public List<EnemyView> Enemies { get; }
    }
}