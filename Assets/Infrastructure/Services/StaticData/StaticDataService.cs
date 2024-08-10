using System.Collections.Generic;
using System.Linq;
using Infrastructure.Constants;
using Infrastructure.Services.StaticData.Level;
using Infrastructure.Services.StaticData.EnemyConfigs;
using Infrastructure.Services.StaticData.PlayerConfigs;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService, ICurrentLevelConfig
    {
        public Dictionary<int, LevelConfig> Levels { get; private set; }
        public Dictionary<EEnemyType, EnemyData> Enemies { get; private set;}
        public LevelConfig CurrentLevelConfig { get; private set; }
        public PlayerMoveConfig PlayerMoveConfig { get; private set; }
        public PlayerHitPointsConfig PlayerHitPointsConfig { get; private set; }

        public StaticDataService()
        {
            LoadLevelsData();
            LoadPlayerMoveConfig();
            LoadPlayerHitPointsConfig();
            LoadEnemyConfig();
        }

        public void LoadLevelsData()
        {
            Levels = Resources
                .LoadAll<LevelStaticData>(AssetPaths.LevelsData)
                .Select(x => x.Config)
                .ToDictionary(x => x.ID, x => x);

            Debug.Log("level data loaded");
        }

        public void LoadPlayerMoveConfig()=>
            PlayerMoveConfig = Resources.Load<PlayerMoveConfig>(AssetPaths.PlayerMoveData);

        public void LoadPlayerHitPointsConfig()=>
            PlayerHitPointsConfig = Resources.Load<PlayerHitPointsConfig>(AssetPaths.PlayerHitPointsData);

        public void LoadEnemyConfig()
        {
            Enemies = Resources
                .LoadAll<EnemyStaticConfig>(AssetPaths.EnemysData)
                .Select(x => x.EnemyData)
                .ToDictionary(x => x.EnemyType, x => x);
            Debug.Log("Enemy data loaded");
        }

        public LevelConfig ForLevel(int id)
        {
            id = CheckForNull(id);
            
            LevelConfig config = Levels.Where(kvp => kvp.Key == id)
                .Select(kvp => kvp.Value)
                .FirstOrDefault();
            
            CurrentLevelConfig = config;
            
            return config;
        }

        private int CheckForNull(int id)
        {
            return id <= 0 ? 1 : id;
        }


        public void CleanUp()
        {
        }

    }
}