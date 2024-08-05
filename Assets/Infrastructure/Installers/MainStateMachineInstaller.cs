using Infrastructure.Factories.StateFactories;
using Infrastructure.States.MainStates;
using Infrastructure.States.StateMachines;
using Zenject;

namespace Infrastructure.Installers
{
    public class MainStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStates();

            Container.BindInterfacesAndSelfTo<MainStateMachine>().AsSingle(); //MainStateMachine entry point is Initialize()
            Container.BindInterfacesAndSelfTo<MainStateFactory>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadGameState>().AsSingle().NonLazy();
        }
    }
}