using System;

namespace Infrastructure.Services.StaticData.Level
{
    [Serializable]
    public class LevelConfig
    {
        public string SceneName;
        public string InGameName;
        public int GameTimer;
        public float SpawnDelay;
        public int CargoGoal;
    }
}
