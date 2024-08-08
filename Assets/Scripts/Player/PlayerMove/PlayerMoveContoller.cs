using Infrastructure.Services.Input;
using Infrastructure.Services.TimerServices;
using UnityEngine;

public class PlayerMoveContoller
{
    private IPlayerParams _playerParams;
    private IPlayerView _playerView;

    private IInputService _inputServise;
    private readonly IStartTimerService _startTimerService;

    private Vector3 _movementDirection;

    public PlayerMoveContoller(
        IPlayerView playerView, 
        IPlayerParams playerParams, 
        IInputService inputService,
        IStartTimerService startTimerService)
    {
        _playerView = playerView;
        _playerParams = playerParams;
        _inputServise = inputService;
        _startTimerService = startTimerService;

        _startTimerService.OnTimerOut += OnStartTimerOut;
        

        _playerView.SetAnimationSpeed(0);
    }

    private void OnStartTimerOut()
    {
        _startTimerService.OnTimerOut -= OnStartTimerOut;
        
        _inputServise.OnInputDirection += Move;
        _inputServise.OnInputDirection += Rotation;
        _inputServise.OnInputDirection += SetAnimation;
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
        _playerView.Move(moveDirection * (_playerParams.MoveSpeed * Time.deltaTime));
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