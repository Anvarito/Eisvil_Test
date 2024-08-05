using Infrastructure.Services.StaticData.Level;

namespace Infrastructure.Services
{
    public interface ICurrentLevelConfig
    {
        LevelConfig CurrentLevelConfig { get; }
    }
}