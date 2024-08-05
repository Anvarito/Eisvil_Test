using Infrastructure.Services.StaticData.Level;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Services.PointGoal
{
    public class PointGoalService : IPointGoalService
    {
        public UnityAction OnPointsGoal { get; set; }

        private LevelConfig _levelConfig;
        private int points = 0;
        
        public PointGoalService(ICurrentLevelConfig levelConfig)
        {
            _levelConfig = levelConfig.CurrentLevelConfig;
        }


        public void WarmUp()
        {
            
        }

        private void Encrease()
        {
            points++;
            if (points >= _levelConfig.CargoGoal)
                OnPointsGoal?.Invoke();
        }

        public void CleanUp()
        {
            points = 0;
        }

        
    }
}