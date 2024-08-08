using Infrastructure.Factories;
using Infrastructure.Services.ClosestEnemy;
using Infrastructure.Services.Input;
using Infrastructure.Services.TimerServices;
using UnityEngine;

namespace Player.PlayerShoot
{
    public class PlayerAimRotating
    {
        private readonly IInputService _inputService;
        private readonly IClosestEnemySearcher _closestEnemySearcher;
        private readonly IPlayerView _playerView;
        private readonly IStartTimerService _startTimerService;

        public PlayerAimRotating(
            IInputService inputService, 
            IClosestEnemySearcher closestEnemySearcher, 
            IPlayerView playerView,
            IStartTimerService startTimerService)
        {
            _inputService = inputService;
            _closestEnemySearcher = closestEnemySearcher;
            _playerView = playerView;
            _startTimerService = startTimerService;

            _startTimerService.OnTimerOut += OnStartTimerOut;
        }

        ~PlayerAimRotating()
        {
            _inputService.OnInputDirection -= Rotation;
        }
        
        private void OnStartTimerOut()
        {
            _startTimerService.OnTimerOut -= OnStartTimerOut;
            _inputService.OnInputDirection += Rotation;
        }

        private void Rotation(Vector3 moveDirection)
        {
            if (moveDirection != Vector3.zero)
                return;

            Transform closestEnemy = _closestEnemySearcher.GetClosestEnemyTransform();
            if (closestEnemy == null)
                return;
            
            _playerView.LookAtPoint(closestEnemy.position);
            
        }
    }
}