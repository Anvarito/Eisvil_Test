using Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

public class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private DamageRecivier _damageRecivier;

    private IHitPoints _hitPoints;
    private IStaticDataService _staticDataService;

    [Inject]
    private void Construct(IStaticDataService staticDataService, IHitPoints hitPoints )
    {
        _staticDataService = staticDataService;
        _hitPoints = hitPoints;
        _damageRecivier.OnApplyDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        _damageRecivier.OnApplyDamage -= TakeDamage;
    }

    private void TakeDamage(int damageAmount)
    {
        print("current hp is " + _hitPoints.CurrentHitPoints);
        if (_hitPoints.DecreaseHitPoints(damageAmount) <= 0)
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        
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
        float alpha = _staticDataService.PlayerMoveConfig.AngularSpeed * Time.deltaTime;
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