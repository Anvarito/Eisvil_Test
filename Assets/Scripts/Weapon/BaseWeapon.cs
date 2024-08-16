using Infrastructure.Services.StaticData.Weapon;
using UnityEngine;

namespace Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponStaticData _weaponStaticData;
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _spawnPoint;
    
        protected float _fireCooldown;
        protected bool _isShootAllow;

        public void Shoot(bool isAllow)
        {
            _isShootAllow = isAllow;
        }
        protected virtual void Update()
        {
            if (_isShootAllow)
            {
                _fireCooldown += Time.deltaTime;
                if (_fireCooldown >= _weaponStaticData.FireRate)
                {
                    SpawnBullet();
                    _fireCooldown = 0;
                }
            }
        }

        protected virtual void SpawnBullet()
        {
            Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
        }
    }
}