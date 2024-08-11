using Infrastructure.Services.ClosestEnemy;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Player
{
    public class PlayerAimRotating
    {
        private readonly IInputService _inputService;
        private readonly IClosestEnemySearcher _closestEnemySearcher;
        private readonly IPlayerView _playerView;

        public PlayerAimRotating(
            IInputService inputService, 
            IClosestEnemySearcher closestEnemySearcher, 
            IPlayerView playerView)
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

        private void Rotation(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
                return;

            Transform closestTarget = _closestEnemySearcher.GetClosestEnemyTransform();
            if (closestTarget == null)
                return;
            
            _playerView.LookAtPoint(closestTarget.position);
            
        }
    }
}
