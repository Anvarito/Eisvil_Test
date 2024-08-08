using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticData;
using UnityEngine;

namespace Infrastructure.Services.GameProgress
{
    public class LevelProgressService : IService, ILevelProgressService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelNumberSaver _levelNumberSaver;
        private readonly ILoggingService _loggingService;
        private readonly ICurrentLevelConfig _currentLevelConfig;

        public LevelProgressService(
            IStaticDataService staticDataService, 
            ILevelNumberSaver levelNumberSaver, 
            ILoggingService loggingService, 
            ICurrentLevelConfig currentLevelConfig)
        {
            _staticDataService = staticDataService;
            _levelNumberSaver = levelNumberSaver;
            _loggingService = loggingService;
            _currentLevelConfig = currentLevelConfig;
        }
        
        public void CleanUp()
        {
            
        }

        public void SaveLevelProgressNumber()
        {
            int level = _currentLevelConfig.CurrentLevelConfig.ID;
            level += 1;
            
            if (level > _staticDataService.Levels.Count)
                level = Random.Range(1, _staticDataService.Levels.Count + 1);
            
            _levelNumberSaver.Save(level);
            _loggingService.LogMessage("Save as level" + level);
        }

        public int GetLevelProgressNumber()
        {
            int level = _levelNumberSaver.Load();
            _loggingService.LogMessage("Level " + level + " load");
            return level;
        }
    }
}