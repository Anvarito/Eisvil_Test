using System.Collections.Generic;
using Infrastructure.Services.StaticData.Level;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadLevelsData();
        void LoadPlayerMoveConfig();
        void LoadEnemyConfig();
        LevelConfig ForLevel(int id);
        Dictionary<int, LevelConfig> Levels { get; }
        Dictionary<EEnemyType, EnemyData> Enemies { get; }
    }
}