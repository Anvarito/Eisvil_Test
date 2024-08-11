using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using Infrastructure.States.Interfaces;
using Infrastructure.States.MainStates;
using Infrastructure.States.StateMachines;
using Player;

namespace Infrastructure.States.InGameStates
{
    public class GameLoose : IState
    {
        private readonly IAssetLoader _assetLoader;
        private readonly MainStateMachine _mainStateMachine;
        private DefeatScreen _defeatCanvas;

        public GameLoose(IAssetLoader assetLoader, MainStateMachine mainStateMachine)
        {
            _assetLoader = assetLoader;
            _mainStateMachine = mainStateMachine;
        }
        
        public void Exit()
        {
        }

        public void Enter()
        {
            _defeatCanvas = _assetLoader.Instantiate<DefeatScreen>(AssetPaths.DefeatCanvas);
            _defeatCanvas.OnCLick += ButtonPush;
        }

        private void ButtonPush()
        {
            _defeatCanvas.OnCLick -= ButtonPush;
            _mainStateMachine.Enter<LoadGameState>();
        }
    }
}