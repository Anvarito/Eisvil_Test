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
        public LevelConfig CurrentLevelConfig { get; private set; }

        public StaticDataService()
        {
            LoadAllStaticData();
        }

        public void LoadAllStaticData()
        {
            Levels = Resources
                .LoadAll<LevelStaticData>(AssetPaths.LevelDataPath)
                .Select(x => x.Config)
                .ToDictionary(x => x.SceneName, x => x);

            Debug.Log("Static data loaded");
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

        public PlayerMoveConfig ForPlayer => Resources.Load<PlayerMoveConfig>(AssetPaths.PlayerDataPath);


        public void CleanUp()
        {
        }

    }
}