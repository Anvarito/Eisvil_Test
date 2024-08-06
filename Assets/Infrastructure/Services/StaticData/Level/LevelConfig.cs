using System;
using UnityEngine.Serialization;

namespace Infrastructure.Services.StaticData.Level
{
    [Serializable]
    public class LevelConfig
    {
        public string ID;
        public string InGameName;
        public int GroundEnemy;
        public int FlyEnemy;
        public int ExplosionEnemy;
    }
}
