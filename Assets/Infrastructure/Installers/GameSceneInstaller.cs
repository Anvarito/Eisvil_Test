using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.Services.PointGoal;
using Infrastructure.Services.StaticData;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    private StaticDataService _staticDataService;

    public override void InstallBindings()
    {
        BindFactories();
        
        Container.BindInterfacesTo<PointGoalService>().AsSingle().NonLazy(); 
        
        //BindCargoFactory();
        ResolveStaticDataService();
        
        BindPlayerData();
        BindPlayerView();
        BindPlayerController();
        
        //BindCameraMover();
    }

    // private void BindCargoFactory()=>
    //     Container.BindFactory<Enemy, Enemy.Factory>().FromComponentInNewPrefabResource(AssetPaths.Enemy);


    private void BindFactories()
    {
        Container.BindInterfacesTo<EnemyFactory>().AsSingle().NonLazy();
    }

    private void ResolveStaticDataService()=>
        _staticDataService = Container.Resolve<StaticDataService>();
    private void BindPlayerData()
    {
        var playerDataModel = new PlayerDataModel(_staticDataService.ForPlayer.Speed,
            _staticDataService.ForPlayer.AngularSpeed);
        Container.BindInterfacesTo<PlayerDataModel>().FromInstance(playerDataModel).AsSingle();
    }

    private void BindPlayerView()=>
        Container.BindInterfacesTo<PlayerView>().FromComponentInNewPrefabResource(AssetPaths.Player).AsSingle();
    
    private void BindPlayerController()=>
        Container.Bind<PlayerContoller>().AsSingle().NonLazy();
    //
    // private void BindCameraMover()=>
    //     Container.Bind<CameraMover>().FromComponentInNewPrefabResource(AssetPaths.PlayerCamera).AsSingle().NonLazy();
}
