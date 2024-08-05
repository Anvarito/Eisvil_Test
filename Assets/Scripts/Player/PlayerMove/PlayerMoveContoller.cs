using Infrastructure.Services.Input;
using UnityEngine;

public class PlayerMoveContoller
{
    private IPlayerDataModel _playerDataModel;
    private IPlayerView _playerView;

    private IInputService _inputServise;

    private Vector3 _movementDirection;

    public PlayerMoveContoller(IPlayerView playerView, IPlayerDataModel playerDataModel, IInputService inputService)
    {
        _playerView = playerView;
        _playerDataModel = playerDataModel;
        _inputServise = inputService;

        _inputServise.OnInputDirection += Move;
        _inputServise.OnInputDirection += Rotation;
        _inputServise.OnInputDirection += SetAnimation;

        _playerView.SetAnimationSpeed(0);
    }

    public void Dispose()
    {
        _playerView.SetAnimationSpeed(0);

        _inputServise.OnInputDirection -= Move;
        _inputServise.OnInputDirection -= Rotation;
        _inputServise.OnInputDirection -= SetAnimation;
    }

    private void Move(Vector3 moveDirection)
    {
        _playerView.Move(moveDirection * (_playerDataModel.MoveSpeed * Time.deltaTime));
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

        var currentRotation = _playerView.Transform.rotation;
        var targetRotation = Quaternion.LookRotation(moveDirection);
        var alpha = _playerDataModel.AngularSpeed * Time.deltaTime;
        var newRotation = Quaternion.Slerp(currentRotation, targetRotation, alpha);
        _playerView.Rotating(newRotation);
    }
}