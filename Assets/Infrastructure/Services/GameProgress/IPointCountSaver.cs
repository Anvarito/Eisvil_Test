namespace Infrastructure.Services.GameProgress
{
    public interface IPointCountSaver
    {
        void Save(int points);
        int Load();
    }
}