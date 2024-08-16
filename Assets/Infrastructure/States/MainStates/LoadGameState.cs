using Infrastructure.SceneManagement;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.StaticData;
using Infrastructure.States.Interfaces;

namespace Infrastructure.States.MainStates
{
    public class LoadGameState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelProgressService _levelProgressService;
        private readonly string _sceneName = "Game";

        public LoadGameState(
            SceneLoader sceneLoader, 
            IStaticDataService staticDataService,
            ILevelProgressService levelProgressService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _levelProgressService = levelProgressService;
        }

        public void Enter()
        {
            var levelNumber = _levelProgressService.GetLevelProgressNumber();
            _staticDataService.ForLevel(levelNumber);
            _sceneLoader.Load(_sceneName);
        }

        public void Exit()
        {
        }
    }
}
