namespace Infrastructure.Services.GameProgress
{
    public interface ILevelNumberSaver
    {
        void Save(int level);
        void SaveOrdinal(int level);
        int Load();
        int LoadOrdinal();
    }
}