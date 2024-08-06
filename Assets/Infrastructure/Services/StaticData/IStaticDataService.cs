using System.Collections.Generic;
using Infrastructure.Services.StaticData.Level;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadLevelsData();
        void LoadPlayerMoveConfig();
        void LoadEnemyConfig();
        LevelConfig ForLevel(string id);
        Dictionary<string, LevelConfig> Levels { get; }
        Dictionary<EEnemyType, EnemyData> Enemies { get; }
    }
}