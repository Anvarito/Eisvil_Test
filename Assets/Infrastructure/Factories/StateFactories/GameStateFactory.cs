using Zenject;

namespace Infrastructure.Factories.StateFactories
{
    public class GameStateFactory : StateFactory
    {
        public GameStateFactory(DiContainer container) : base(container)
        {
        }
    }
}
