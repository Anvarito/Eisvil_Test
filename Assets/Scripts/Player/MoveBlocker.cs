namespace Player
{
    public class MoveBlocker
    {
        private readonly PlayerMove _playerMove;
        private readonly PlayerAimRotator _playerAimRotator;
        private readonly PlayerShoot _playerShoot;

        public MoveBlocker(
            PlayerMove moveController, 
            PlayerAimRotator aimRotator, 
            PlayerShoot playerShoot)
        {
            _playerMove = moveController;
            _playerAimRotator = aimRotator;
            
            _playerShoot = playerShoot;
            
            DisableControl();
        }

        public void EnableControl()
        {
            _playerMove.EnableMove();
            _playerAimRotator.EnableAiming();
            _playerShoot.EnableShooting(true);
        }

        public void DisableControl()
        {
            _playerMove.BlockMove();
            _playerAimRotator.BlockAiming();
            _playerShoot.EnableShooting(false);
        }
    }
}