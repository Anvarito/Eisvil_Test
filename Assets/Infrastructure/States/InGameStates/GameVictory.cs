using Cysharp.Threading.Tasks;
using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Logging;
using Infrastructure.States.Interfaces;
using Infrastructure.States.MainStates;
using Infrastructure.States.StateMachines;
using UnityEngine;

namespace Infrastructure.States.InGameStates
{
    public class GameVictory : IState
    {
        private PlayerMoveContoller _playerMoveContoller;
        private IAssetLoader _assetLoader;
        private readonly ILoggingService _loggingService;
        private MainStateMachine _mainStateMachine;
        private VictoryScreen _victoryScreen;

        public GameVictory(
            MainStateMachine mainStateMachine
            , PlayerMoveContoller playerMoveContoller
            , IAssetLoader assetLoader
            , ILoggingService loggingService
        )
        {
            _mainStateMachine = mainStateMachine;
            _assetLoader = assetLoader;
            _loggingService = loggingService;
            _playerMoveContoller = playerMoveContoller;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _playerMoveContoller.Dispose();
            _victoryScreen = _assetLoader.Instantiate<VictoryScreen>(AssetPaths.VictoryCanvas);
            _victoryScreen.OnCLick += ClaimPush;
        }

        private void ClaimPush()
        {
            _victoryScreen.OnCLick -= ClaimPush;
            _loggingService.LogMessage("CLAIM");
            _mainStateMachine.Enter<LoadGameState>();
        }

    }
}