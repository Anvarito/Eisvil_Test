using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.PointGoal;
using Infrastructure.Services.PointScore;
using Infrastructure.Services.StaticData;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        private StaticDataService _staticDataService;

        public override void InstallBindings()
        {
            BindFactories();

            Container.BindInterfacesTo<PointScoreService>().AsSingle().NonLazy();

            ResolveStaticDataService();

            BindPlayerData();
            BindPlayerView();
            BindPlayerController();


            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
            BindEnemyFactory();
            //BindCameraMover();
        }

         private void BindEnemyFactory()
         {
             Container.BindInterfacesTo<EnemyFactory>().AsSingle().NonLazy();
         }


        private void BindFactories()
        {
            //Container.BindInterfacesTo<EnemyFactory>().AsSingle().NonLazy();
        }

        private void ResolveStaticDataService() =>
            _staticDataService = Container.Resolve<StaticDataService>();

        private void BindPlayerData()
        {
            var playerDataModel = new PlayerDataModel(_staticDataService.PlayerMoveConfig.Speed,
                _staticDataService.PlayerMoveConfig.AngularSpeed);
            Container.BindInterfacesTo<PlayerDataModel>().FromInstance(playerDataModel).AsSingle();
        }

        private void BindPlayerView() =>
            Container.BindInterfacesTo<PlayerView>().FromComponentInNewPrefabResource(AssetPaths.PlayerPrefab).AsSingle();

        private void BindPlayerController() =>
            Container.Bind<PlayerMoveContoller>().AsSingle().NonLazy();
        //
        // private void BindCameraMover()=>
        //     Container.Bind<CameraMover>().FromComponentInNewPrefabResource(AssetPaths.PlayerCamera).AsSingle().NonLazy();
    }
}
