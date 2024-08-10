using System.Collections.Generic;
using Infrastructure.Services.StaticData.EnemyConfigs;
using Infrastructure.Services.StaticData.Level;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        LevelConfig ForLevel(int id);
        public PlayerConfigs.PlayerMoveConfig PlayerMoveConfig { get; }
        public PlayerConfigs.PlayerHitPointsConfig PlayerHitPointsConfig { get; }
        Dictionary<int, LevelConfig> Levels { get; }
        Dictionary<EEnemyType, EnemyData> Enemies { get; }
    }
}