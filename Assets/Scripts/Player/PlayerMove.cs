using Infrastructure.Services.Input;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Player
{
    public class PlayerMove
    {
        private IStaticDataService _staticDataService;
        private IPlayerView _playerView;

        private IInputService _inputServise;

        private Vector3 _movementDirection;

        public PlayerMove(
            IPlayerView playerView, 
            IStaticDataService staticDataService,
            IInputService inputService)
        {
            _playerView = playerView;
            _staticDataService = staticDataService;
            _inputServise = inputService;

            _playerView.SetAnimationSpeed(0);
        }

        public void EnableMove()
        {
            _inputServise.OnInputDirection += Move;
            _inputServise.OnInputDirection += Rotation;
            _inputServise.OnInputDirection += SetAnimation;
        }

        public void BlockMove()
        {
            _playerView.SetAnimationSpeed(0);

            _inputServise.OnInputDirection -= Move;
            _inputServise.OnInputDirection -= Rotation;
            _inputServise.OnInputDirection -= SetAnimation;
        }

        private void Move(Vector3 moveDirection)
        {
            _playerView.Move(moveDirection * (_staticDataService.PlayerMoveConfig.Speed * Time.deltaTime));
        }

        private void SetAnimation(Vector3 moveDirection)
        {
            var normalizedMagnitude = Mathf.Clamp01(moveDirection.magnitude);
            _playerView.SetAnimationSpeed(normalizedMagnitude);
        }

        private void Rotation(Vector3 moveDirection)
        {
            if (moveDirection == Vector3.zero)
                return;

            _playerView.RotatingTo(moveDirection);
        }
    }
}