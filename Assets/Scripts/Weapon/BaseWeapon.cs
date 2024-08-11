using Infrastructure.Services.StaticData.Weapon;
using UnityEngine;

namespace Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponStaticData _weaponStaticData;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _spawnPoint;
    
        private float _fireCooldown;
        private bool _isShootAllow;

        public void Shoot(bool isAllow)
        {
            _isShootAllow = isAllow;
        }
        private void Update()
        {
            if (_isShootAllow)
            {
                _fireCooldown += Time.deltaTime;
                if (_fireCooldown >= _weaponStaticData.FireRate)
                {
                    Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
                    _fireCooldown = 0;
                }
            }
        }
    }
}