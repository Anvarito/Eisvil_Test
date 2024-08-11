using Infrastructure.Constants;
using Infrastructure.Services.StaticData;
using Player;
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
        CreatePlayerFacade();
    }

    public void CreatePlayerFacade()
    {
        IHitPoints hitPointsPlayer = new HitPointsHolder(_staticDataService.PlayerHitPointsConfig.HitPoints);
        Container.Bind<PlayerController>().AsSingle()
            .WithArguments(hitPointsPlayer).NonLazy();
    }

    private void BindPlayerController() =>
        Container.Bind<PlayerMove>().AsSingle().NonLazy();


    private void BindPlayerView() => 
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInNewPrefabResource(AssetPaths.PlayerPrefab)
        .AsSingle().NonLazy();

    private void BindPlayerAim() =>
        Container.Bind<PlayerAimRotating>().AsSingle().NonLazy();

    private void ResolveStaticDataService() =>
        _staticDataService = Container.Resolve<StaticDataService>();
}