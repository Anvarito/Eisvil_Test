using Infrastructure.Services.TimerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerController
    {
        public UnityAction OnDead;
        
        private readonly IHitPoints _hitPoints;
        private readonly PlayerMove _playerMove;
        private readonly PlayerAimRotating _playerAimRotating;
        private readonly PlayerShoot.PlayerShoot _playerShoot;
        private readonly IStartTimerService _startTimerService;
        private readonly DamageRecivier _damageRecivier;
        private readonly PlayerView _playerView;

        public PlayerController(
            PlayerMove moveController, 
            PlayerAimRotating aimRotating, 
            PlayerView playerView, 
            IStartTimerService startTimerService, 
            IHitPoints hitPoints)
        {
            _hitPoints = hitPoints;
            _playerMove = moveController;
            _playerAimRotating = aimRotating;
            _playerView = playerView;
            _startTimerService = startTimerService;
            
            _damageRecivier = _playerView.DamageRecivier;
            _playerShoot = _playerView.PlayerShoot;
            
            DisableControl();
            
            _startTimerService.OnTimerOut += EnableControl;
            _damageRecivier.OnApplyDamage += TakeDamage;
        }

        private void EnableControl()
        {
            _startTimerService.OnTimerOut -= EnableControl;
            
            _playerMove.EnableMove();
            _playerAimRotating.EnableAiming();
            _playerShoot.enabled = true;
        }

        public void DisableControl()
        {
            _playerMove.BlockMove();
            _playerAimRotating.BlockAiming();
            _playerShoot.enabled = false;
        }

        private void TakeDamage(int damageAmount)
        {
            Debug.Log("HP is " + _hitPoints.CurrentHitPoints);
            if (_hitPoints.DecreaseHitPoints(damageAmount) <= 0)
            {
                Dead();
            }
        }

        private void Dead()
        {
            _damageRecivier.OnApplyDamage -= TakeDamage;
            DisableControl();
            OnDead?.Invoke();
        }
    }

}