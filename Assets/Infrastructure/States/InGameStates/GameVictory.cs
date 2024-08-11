using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.Logging;
using Infrastructure.States.Interfaces;
using Infrastructure.States.MainStates;
using Infrastructure.States.StateMachines;
using Player;

namespace Infrastructure.States.InGameStates
{
    public class GameVictory : IState
    {
        private IAssetLoader _assetLoader;
        private readonly ILevelProgressService _levelProgressService;
        private readonly ILoggingService _loggingService;
        private MainStateMachine _mainStateMachine;
        private VictoryScreen _victoryScreen;

        public GameVictory(
            MainStateMachine mainStateMachine
            , IAssetLoader assetLoader
            , ILevelProgressService levelProgressService
            , ILoggingService loggingService
        )
        {
            _mainStateMachine = mainStateMachine;
            _assetLoader = assetLoader;
            _levelProgressService = levelProgressService;
            _loggingService = loggingService;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _levelProgressService.SaveLevelProgressNumber();
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