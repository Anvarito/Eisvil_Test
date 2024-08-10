using UnityEngine;

namespace Infrastructure.Services.StaticData.PlayerConfigs
{
    [CreateAssetMenu(fileName = "PlayerHitPointsConfig", menuName = "ScriptableObjects/PlayerHitPointsConfig", order = 1)]
    public class PlayerHitPointsConfig : ScriptableObject
    {
        public int HitPoints;
    }
}