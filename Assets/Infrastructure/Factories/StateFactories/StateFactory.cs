using Infrastructure.States;
using Zenject;

namespace Infrastructure.Factories.StateFactories
{
    public abstract class StateFactory
    {
        protected readonly DiContainer _container;
        public StateFactory(DiContainer container) =>
            _container = container;
        public T CreateState<T>() where T : IExitableState =>
            _container.Resolve<T>();
    }
}