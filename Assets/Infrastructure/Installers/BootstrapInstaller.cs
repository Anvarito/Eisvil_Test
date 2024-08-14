using Infrastructure.SceneManagement;
using Infrastructure.Services.Assets;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.Input;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.Level;
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
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle().NonLazy(); 
            Container.BindInterfacesTo<LevelProgressService>().AsSingle().NonLazy(); 
            Container.BindInterfacesTo<LevelNumberSaver>().AsSingle().NonLazy(); 
            Container.BindInterfacesTo<PointCountSaver>().AsSingle().NonLazy(); 
        }
    }
}
