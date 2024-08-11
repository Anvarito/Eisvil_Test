using Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerShoot.PlayerShoot _playerShoot;
        [SerializeField] private DamageRecivier _damageRecivier;
        public PlayerShoot.PlayerShoot PlayerShoot => _playerShoot;
        public DamageRecivier DamageRecivier => _damageRecivier;
    
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
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
}