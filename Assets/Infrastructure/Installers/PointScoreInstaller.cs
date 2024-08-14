using Infrastructure.Services.PointScore;
using Zenject;

namespace Infrastructure.Installers
{
    public class PointScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointScoreService>().AsSingle().NonLazy();
        }
    }
}