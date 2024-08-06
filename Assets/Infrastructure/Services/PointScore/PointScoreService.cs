using Infrastructure.Services.PointGoal;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.StaticData.Level;
using UnityEngine.Events;

namespace Infrastructure.Services.PointScore
{
    public class PointScoreService : IPointScoreService
    {

        private LevelConfig _levelConfig;
        private int points = 0;
        
        public PointScoreService(ICurrentLevelConfig levelConfig)
        {
            _levelConfig = levelConfig.CurrentLevelConfig;
        }


        public void WarmUp()
        {
            
        }

        private void Encrease()
        {
            points++;
        }

        public void CleanUp()
        {
            points = 0;
        }

        
    }
}
