using UnityEngine;

namespace Infrastructure.Services.StaticData.Weapon
{
    [CreateAssetMenu(menuName = "Static Data/WeaponStaticData", fileName = "WeaponStaticData")]

    public class WeaponStaticData : ScriptableObject
    {
        public float FireRate;
    }
}