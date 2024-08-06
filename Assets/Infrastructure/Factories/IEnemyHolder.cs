using System.Collections.Generic;

namespace Infrastructure.Factories
{
    public interface IEnemyHolder
    {
        public List<Enemy> Enemies { get; }
    }
}