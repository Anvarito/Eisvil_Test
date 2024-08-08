using System;
using UnityEngine.Serialization;

namespace Infrastructure.Services.StaticData.Level
{
    [Serializable]
    public class LevelConfig
    {
        public int ID;
        public int GroundEnemyCount;
        public int FlyEnemyCount;
        public int ExplosionEnemyCount;
    }
}
