using Infrastructure.Services.Input;
using Infrastructure.Services.TimerServices;
using UnityEngine;
using Zenject;

namespace Player.PlayerShoot
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _muzzlePoint;
        
        private bool _isStay = false;
        private bool _isStartTimerOver = false;
        private float _fireRate = 0.5f;
        private float _fireCooldown = 0;
        
        private IInputService _inputService;
        private IStartTimerService _startTimerService;

        [Inject]
        private void Construct(
            IInputService inputService,
            IStartTimerService startTimerService)
        {
            _startTimerService = startTimerService;
            _inputService = inputService;
        }

        private void Awake()
        {
            _fireCooldown = _fireRate;
            _startTimerService.OnTimerOut += OnStartTimerOut;
            _inputService.OnInputDirection += OnInputDirection;
        }

        private void OnStartTimerOut()
        {
            _startTimerService.OnTimerOut -= OnStartTimerOut;
            _isStartTimerOver = true;
        }

        private void OnDestroy()
        {
            _inputService.OnInputDirection -= OnInputDirection;
        }

        private void OnInputDirection(Vector3 direction)
        {
            _isStay = direction == Vector3.zero;
        }

        private void Update()
        {
            if (_isStay && _isStartTimerOver)
            {
                _fireCooldown += Time.deltaTime;
                if (_fireCooldown >= _fireRate)
                {
                    Instantiate(_bulletPrefab,_muzzlePoint.position, _muzzlePoint.rotation);
                    _fireCooldown = 0;
                }
            }
        }
    }
}