using Infrastructure.Services.Input;
using Infrastructure.Services.NavMeshBuild;
using Infrastructure.Services.TimerServices;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<WASDinputHadler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<StartTimerService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<NavMeshRebuildService>().AsSingle().NonLazy();
        }

    }
}