using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using Infrastructure.Services.StaticData;
using Player.PlayerShoot;
using Zenject;

namespace Infrastructure.Factories
{
    public class PlayerFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetLoader _assetLoader;
        private StaticDataService _staticDataService;
        private PlayerView _playerView;
        private PlayerMoveContoller _playerMoveContoller;
        private PlayerAimRotating _playerAimRotating;

        public PlayerFactory(DiContainer container, IAssetLoader assetLoader)
        {
            _container = container;
            _assetLoader = assetLoader;
        }

        public void Create()
        {
            ResolveStaticDataService();
            
            IHitPoints hitPointsPlayer = new HitPointsHolder(_staticDataService.PlayerHitPointsConfig.HitPoints);
            
            _playerView = _assetLoader.Instantiate<PlayerView>(AssetPaths.PlayerPrefab);
            //_playerMoveContoller = new PlayerMoveContoller();
        }

        private void BindPlayerView()
        {
            
        }

        private void BindPlayerController() =>
            _container.Bind<PlayerMoveContoller>().AsSingle().NonLazy();
    
        private void BindPlayerAim() =>
            _container.Bind<PlayerAimRotating>().AsSingle().NonLazy();
    
        private void ResolveStaticDataService() =>
            _staticDataService = _container.Resolve<StaticDataService>();
    }
}