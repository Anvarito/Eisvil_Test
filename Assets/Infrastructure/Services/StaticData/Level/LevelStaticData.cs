using UnityEngine;

namespace Infrastructure.Services.StaticData.Level
{
    [CreateAssetMenu(menuName = "Static Data/Level Static Data",fileName = "LevelsData")]
    public class LevelStaticData : ScriptableObject
    {
        public LevelConfig Config;
    }
}