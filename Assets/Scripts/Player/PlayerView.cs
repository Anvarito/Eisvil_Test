using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.PlayerConfigs;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour, IPlayerView, IPlayerPosition
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Animator _animator;
        [SerializeField] private DamageRecivier _damageRecivier;

        public Transform PlayerTransform => transform;

        private PlayerMoveConfig _playerMoveConfig;
        private Health _health;

        [Inject]
        public void SetStaticData(IStaticDataService staticDataService, [Inject(Id = "Player health")] Health health,
            Transform spawnPoint)
        {
            _health = health;
            _playerMoveConfig = staticDataService.PlayerMoveConfig;

            transform.position = spawnPoint.position;
            _damageRecivier.OnApplyDamage += TakeDamage;
        }

        private void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
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
            float alpha = _playerMoveConfig.AngularSpeed * Time.deltaTime;
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
            if (!_controller.isGrounded)
                _controller.Move(Vector3.down * (-Physics.gravity.y * Time.deltaTime));
        }
    }
}