using Infrastructure.Extras;

namespace Infrastructure.Factories
{
    public interface IEnemyListHolder
    {
        public ReactiveList<EnemyElement> Enemies { get; }
    }
}