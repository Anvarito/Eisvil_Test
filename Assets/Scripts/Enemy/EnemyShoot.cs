using Weapon;

namespace Enemy
{
    public class EnemyShoot
    {
        private readonly BaseWeapon _weapon;
        public bool HaveGun => _weapon != null;
        public EnemyShoot(BaseWeapon weapon)
        {
            _weapon = weapon;
        }


        public void Shoot()
        {
            if(_weapon)
                _weapon.Shoot(true);
        }

        public void StopShooting()
        {
            if(_weapon)
                _weapon.Shoot(false);
        }
    }
}