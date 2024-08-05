using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using Infrastructure.States.MainStates;
using Infrastructure.States.StateMachines;

namespace Infrastructure.States.InGameStates
{
    public class GameVictory : IState
    {
        private PlayerContoller _playerContoller;
        private IAssetLoader _assetLoader;
        private MainStateMachine _mainStateMachine;

        public GameVictory(
            MainStateMachine mainStateMachine
            , PlayerContoller playerContoller
            , IAssetLoader assetLoader
        )
        {
            _mainStateMachine = mainStateMachine;
            _assetLoader = assetLoader;
            _playerContoller = playerContoller;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _playerContoller.Dispose();
        }

        private void PressAccepted()
        {
            //_mainStateMachine.Enter<MenuState>();
        }
    }
}