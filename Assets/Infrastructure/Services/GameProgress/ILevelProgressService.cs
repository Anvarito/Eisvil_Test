namespace Infrastructure.Services.GameProgress
{
    public interface ILevelProgressService
    {
        void SaveLevelProgressNumber();
        int GetLevelProgressNumber();
    }
}