using Infrastructure.Factories;
using Infrastructure.Services.ClosestEnemy;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Player.PlayerShoot
{
    public class PlayerAimRotating
    {
        private readonly IPlayerDataModel _playerDataModel;
        private readonly IInputService _inputService;
        private readonly ISearchClosestEnemy _searchClosestEnemy;
        private readonly IPlayerView _playerView;

        public PlayerAimRotating(IPlayerDataModel playerDataModel, IInputService inputService, ISearchClosestEnemy searchClosestEnemy, IPlayerView playerView)
        {
            _playerDataModel = playerDataModel;
            _inputService = inputService;
            _searchClosestEnemy = searchClosestEnemy;
            _playerView = playerView;
            _inputService.OnInputDirection += Rotation;
        }
        
        private void Rotation(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
                return;

            Transform closestEnemy = _searchClosestEnemy.GetClosestEnemyTransform();
            if (closestEnemy == null)
                return;
            
            Vector3 direction = (closestEnemy.position - _playerView.Transform.position).normalized;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion newRot = Quaternion.Slerp(_playerView.Transform.rotation, targetRotation, _playerDataModel.AngularSpeed * Time.deltaTime);
            _playerView.Rotating(newRot);
            
        }
    }
}