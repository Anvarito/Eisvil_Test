using Infrastructure.Services.Input;
using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerShoot
    {
        private IInputService _inputService;
        private readonly BaseWeapon _weapon;

        public PlayerShoot(IInputService inputService, PlayerView playerView)
        {
            _inputService = inputService;
            _weapon = playerView.GetComponentInChildren<BaseWeapon>();
            _inputService.OnInputDirection += OnInputDirection;
        }

        private void OnInputDirection(Vector2 direction)
        {
            if (_weapon)
                _weapon.Shoot(direction == Vector2.zero);
        }

        public void EnableShooting(bool isEnable)
        {
            if (_weapon)
                _weapon.enabled = isEnable;
        }
    }
}