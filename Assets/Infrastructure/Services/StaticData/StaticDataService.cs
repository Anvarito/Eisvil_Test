using System.Collections.Generic;
using System.Linq;
using Infrastructure.Constants;
using Infrastructure.Services.StaticData.Level;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService, ICurrentLevelConfig
    {
        public Dictionary<string, LevelConfig> Levels { get; private set; }
        public Dictionary<EEnemyType, EnemyData> Enemies { get; private set;}
        public LevelConfig CurrentLevelConfig { get; private set; }
        public PlayerMoveConfig PlayerMoveConfig { get; private set; }

        public StaticDataService()
        {
            LoadLevelsData();
            LoadPlayerMoveConfig();
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

        public void LoadPlayerMoveConfig()
        {
            PlayerMoveConfig = Resources.Load<PlayerMoveConfig>(AssetPaths.PlayerMoveData);
        }

        public void LoadEnemyConfig()
        {
            Enemies = Resources
                .LoadAll<EnemyStaticData>(AssetPaths.EnemysData)
                .Select(x => x.EnemyData)
                .ToDictionary(x => x.EnemyType, x => x);
            Debug.Log("Enemy data loaded");
        }

        public LevelConfig ForLevel(string id)
        {
            if (Levels.TryGetValue(id, out LevelConfig config))
            {
                CurrentLevelConfig = config;
                return CurrentLevelConfig;
            }

            return null;
        }



        public void CleanUp()
        {
        }

    }
}