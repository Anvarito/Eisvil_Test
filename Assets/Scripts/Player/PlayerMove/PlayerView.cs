using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    private IPlayerParams _playerParams;
   
    [Inject]
    private void Construct(IPlayerParams playerParams)
    {
        _playerParams = playerParams;
    }
    
    public void SetAnimationSpeed(float speed)
    {
        _animator.SetFloat("Speed_f", speed);
    }

    public void Move(Vector3 direction)
    {
        _controller.Move(direction);
    }

    public void RotatingTo(Vector3 direction)
    {
        direction.y = 0;
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        float alpha = _playerParams.AngularSpeed * Time.deltaTime;
        Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, alpha);
        
        transform.rotation = newRotation;
    }

    public void LookAtPoint(Vector3 point)
    {
        Vector3 direction = (point - transform.position).normalized;
        RotatingTo(direction);
    }

    private void Update()
    {
        if(!_controller.isGrounded)
        _controller.Move(Vector3.down * (-Physics.gravity.y * Time.deltaTime));
    }
}