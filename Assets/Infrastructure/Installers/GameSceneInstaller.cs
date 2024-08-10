using Infrastructure.Constants;
using Infrastructure.Factories;
using Infrastructure.Services.ClosestEnemy;
using Infrastructure.Services.Input;
using Infrastructure.Services.PointScore;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.TimerServices;
using Player.PlayerShoot;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindEnemyFactory();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<PointScoreService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InputService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ClosestEnemySearcher>().AsSingle().NonLazy();
            Container.BindInterfacesTo<StartTimerService>().AsSingle().NonLazy();
        }

        private void BindEnemyFactory()
        {
            Container.BindInterfacesTo<EnemyHolderFactory>().AsSingle().NonLazy();
        }
    }
}