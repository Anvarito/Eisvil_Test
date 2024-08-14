using Cysharp.Threading.Tasks;

namespace Infrastructure.Factories
{
    public interface IFactory
    {
        UniTask WarmUp();
        void CleanUp();

    }
}
