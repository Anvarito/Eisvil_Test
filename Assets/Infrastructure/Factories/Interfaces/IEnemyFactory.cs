using Cysharp.Threading.Tasks;

namespace Infrastructure.Factories.Interfaces
{
    public interface IEnemyFactory : IFactory
    {
        UniTask SpawnEnemy();
    }
}
