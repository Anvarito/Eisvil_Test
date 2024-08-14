namespace Infrastructure.Services.GameProgress
{
    public interface ILevelProgressService
    {
        void SaveLevelProgressNumber();
        int GetLevelProgressNumber();

        void SavePoints(int points);

        int LoadPoints();
    }
}