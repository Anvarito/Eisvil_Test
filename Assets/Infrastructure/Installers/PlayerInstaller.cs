using Infrastructure.Constants;
using Infrastructure.Services.StaticData;
using Player.PlayerShoot;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    private StaticDataService _staticDataService;

    public override void InstallBindings()
    {
        ResolveStaticDataService();
        
        BindPlayerView();
        BindPlayerController();
        BindPlayerAim();
    }
    
    private void ResolveStaticDataService() =>
        _staticDataService = Container.Resolve<StaticDataService>();
    
    private void BindPlayerView()
    {
        IHitPoints hitPointsPlayer = new HitPointsHolder(_staticDataService.PlayerHitPointsConfig.HitPoints);
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInNewPrefabResource(AssetPaths.PlayerPrefab).AsSingle()
            .WithArguments(hitPointsPlayer);
    }

    private void BindPlayerController() =>
        Container.Bind<PlayerMoveContoller>().AsSingle().NonLazy();
    
    private void BindPlayerAim() =>
        Container.Bind<PlayerAimRotating>().AsSingle().NonLazy();
    
    
}
