
using Infrastructure.Services.Assets;
using Infrastructure.States.Interfaces;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameVictory : IState
    {
        private PlayerMoveContoller _playerMoveContoller;
        private IAssetLoader _assetLoader;
        private MainStateMachine _mainStateMachine;

        public GameVictory(
            MainStateMachine mainStateMachine
            , PlayerMoveContoller playerMoveContoller
            , IAssetLoader assetLoader
        )
        {
            _mainStateMachine = mainStateMachine;
            _assetLoader = assetLoader;
            _playerMoveContoller = playerMoveContoller;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _playerMoveContoller.Dispose();
        }

        private void PressAccepted()
        {
            //_mainStateMachine.Enter<MenuState>();
        }
    }
}