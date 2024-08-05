using Cysharp.Threading.Tasks;

namespace Infrastructure.Factories.Interfaces
{
    public interface IFactory
    {
        UniTask WarmUp();
        void CleanUp();

    }
}
