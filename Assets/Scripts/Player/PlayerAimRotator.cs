using Infrastructure.Services.ClosestEnemy;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerAimRotator
    {
        private readonly IInputService _inputService;
        private readonly IClosestEnemySearcher _closestEnemySearcher;
        private readonly PlayerView _playerView;

        public PlayerAimRotator(
            IInputService inputService, 
            IClosestEnemySearcher closestEnemySearcher, 
            PlayerView playerView)
        {
            _inputService = inputService;
            _closestEnemySearcher = closestEnemySearcher;
            _playerView = playerView;
        }
        public void EnableAiming()
        {
            _inputService.OnInputDirection += Rotation;
        } 
        public void BlockAiming()
        {
            _inputService.OnInputDirection -= Rotation;
        }

        private void Rotation(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
                return;

            Transform closestTarget = _closestEnemySearcher.GetClosestEnemyTransform(_playerView.transform);
            if (closestTarget == null)
                return;
            
            _playerView.LookAtPoint(closestTarget.position);
        }
    }
}
