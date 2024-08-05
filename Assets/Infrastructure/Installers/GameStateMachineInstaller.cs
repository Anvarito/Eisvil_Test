using Infrastructure.Factories.StateFactories;
using Infrastructure.States.InGameStates;
using Infrastructure.States.StateMachines;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStates();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy(); 
            Container.Bind<GameStateFactory>().AsSingle().NonLazy(); 
        }

        private void BindStates()
        {
            Container.Bind<GameWarmUpState>().AsSingle().NonLazy();
            Container.Bind<GameLoop>().AsSingle().NonLazy();
            Container.Bind<GameVictory>().AsSingle().NonLazy();
        }
    }
}
