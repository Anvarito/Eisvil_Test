using System;
using Enemy;

namespace Infrastructure.Services.StaticData.EnemyConfigs
{
    [Serializable]
    public class EnemyData
    {
        public EEnemyType EnemyType;
        public int KillPoints;
        public int HitPoints;
        public float DetectRadius;
        public float ShootDistance;
        public float FireRate;
        public float MoveSpeed;
        public float AngularSpeed;
    
    }
}