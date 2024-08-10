using UnityEngine;

namespace Infrastructure.Services.StaticData.EnemyConfigs
{
    [CreateAssetMenu(fileName = "EnemyStaticConfig", menuName = "ScriptableObjects/EnemyStaticConfig", order = 1)]
    public class EnemyStaticConfig : ScriptableObject
    {
        public EnemyData EnemyData;
    }
}