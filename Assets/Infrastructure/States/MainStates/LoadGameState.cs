using Infrastructure.SceneManagement;
using Infrastructure.States.Interfaces;

namespace Infrastructure.States.MainStates
{
    public class LoadGameState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly string _sceneName = "Game";

        public LoadGameState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(_sceneName);
        }

        public void Exit()
        {
        }

        private async void OnLoaded()
        {
            // _pointGoalService.Setup(_pendingStageStaticData);
            // await _cargoFactory.WarmUp();
            // await _uiFactory.WarmUp();
            // await _heroFactory.WarmUp();
            // _stateMachine.Enter<GameLoopState>();
        }
    }
}
