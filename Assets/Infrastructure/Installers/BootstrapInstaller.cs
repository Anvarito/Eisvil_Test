using Infrastructure.SceneManagement;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticData;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindServices();
        }

        private void BindSceneLoader()=>
            Container.Bind<SceneLoader>().AsSingle();

        private void BindServices()
        {
            Container.Bind<ILoggingService>().To<LoggingService>().AsSingle().NonLazy();
            Container.Bind<IAssetLoader>().To<AssetLoader>().AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle().NonLazy(); 
            
        }
    }
}