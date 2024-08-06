using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.Services.ClosestEnemy;
using Infrastructure.Services.Input;
using Infrastructure.Services.PointScore;
using Infrastructure.Services.StaticData;
using Player.PlayerShoot;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        private StaticDataService _staticDataService;

        public override void InstallBindings()
        {
            ResolveStaticDataService();
            BindPlayerData();
            BindPlayerView();
            BindPlayerController();
            BindPlayerAim();
            BindServices();
            BindEnemyFactory();
            //BindCameraMover();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<PointScoreService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SearchClosestEnemy>().AsSingle().NonLazy();
        }

        private void BindEnemyFactory()
         {
             Container.BindInterfacesTo<EnemyHolderFactory>().AsSingle().NonLazy();
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
        
        private void BindPlayerAim() =>
            Container.Bind<PlayerAimRotating>().AsSingle().NonLazy();
        //
        // private void BindCameraMover()=>
        //     Container.Bind<CameraMover>().FromComponentInNewPrefabResource(AssetPaths.PlayerCamera).AsSingle().NonLazy();
    }
}
