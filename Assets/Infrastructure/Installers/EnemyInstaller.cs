using Infrastructure.Factories;
using Infrastructure.Services.ClosestEnemy;
using Zenject;

namespace Infrastructure.Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEnemyFactory();
        }
        private void BindEnemyFactory()
        {
            Container.BindInterfacesTo<EnemyFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ClosestEnemySearcher>().AsSingle();
        }
    }
}