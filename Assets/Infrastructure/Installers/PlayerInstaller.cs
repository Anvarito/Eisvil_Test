using Infrastructure.Constants;
using Infrastructure.Services.StaticData;
using Player;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    private StaticDataService _staticDataService;
    public Transform PlayerSpawnPoint;
    public override void InstallBindings()
    {
        ResolveStaticDataService();
        BindPlayerView();
        BindPlayerHealth();
        BindPlayerMove();
        BindPlayerAim();
        BindPlayerShoot();
        BindMoveBlocker();
    }

    private void BindMoveBlocker() =>
        Container.Bind<MoveBlocker>().AsSingle().NonLazy();

    private void BindPlayerHealth() =>
        Container.Bind<Health>().WithId("Player health").AsSingle().WithArguments(_staticDataService.PlayerHitPointsConfig.HitPoints).NonLazy();

    private void BindPlayerMove() =>
        Container.Bind<PlayerMove>().AsSingle().NonLazy();

    private void BindPlayerShoot() =>
        Container.Bind<PlayerShoot>().AsSingle().NonLazy();

    private void BindPlayerView() =>
        Container.BindInterfacesAndSelfTo<PlayerView>().FromComponentInNewPrefabResource(AssetPaths.PlayerPrefab).AsSingle().WithArguments(PlayerSpawnPoint)
            .NonLazy();

    private void BindPlayerAim() =>
        Container.Bind<PlayerAimRotator>().AsSingle().NonLazy();

    private void ResolveStaticDataService() =>
        _staticDataService = Container.Resolve<StaticDataService>();
}