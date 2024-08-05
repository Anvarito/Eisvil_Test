using Zenject;

namespace Infrastructure.Factories.StateFactories
{
    public class MainStateFactory : StateFactory
    {
        

        public MainStateFactory(DiContainer container) : base(container)
        {
        }
    }
}