using System;
using Infrastructure.Services.Input;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Player.PlayerShoot
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _muzzlePoint;
        private IInputService _inputService;
        private bool _isStay = false;
        private float _fireRate = 0.5f;
        private float _fireCooldown = 0;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _fireCooldown = _fireRate;
            _inputService.OnInputDirection += OnInputDirection;
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
            if (_isStay)
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