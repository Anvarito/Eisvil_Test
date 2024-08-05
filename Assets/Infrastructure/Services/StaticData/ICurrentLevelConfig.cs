using Infrastructure.Services.StaticData.Level;

namespace Infrastructure.Services.StaticData
{
    public interface ICurrentLevelConfig
    {
        LevelConfig CurrentLevelConfig { get; }
    }
}
