using UnityEngine;

namespace Infrastructure.Services.StaticData.PlayerConfigs
{
    [CreateAssetMenu(fileName = "PlayerHitPointsConfig", menuName = "ScriptableObjects/PlayerHitPointsConfig", order = 1)]
    public class HitPointsConfig : ScriptableObject
    {
        public int HitPoints;
    }
}