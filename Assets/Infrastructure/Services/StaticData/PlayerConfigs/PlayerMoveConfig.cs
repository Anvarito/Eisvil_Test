using UnityEngine;

namespace Infrastructure.Services.StaticData.PlayerConfigs
{
    [CreateAssetMenu(fileName = "PlayerMoveConfig", menuName = "ScriptableObjects/PlayerMoveConfig", order = 1)]
    public class PlayerMoveConfig : ScriptableObject
    {
        public float AngularSpeed;
        public float Speed;
    }
}