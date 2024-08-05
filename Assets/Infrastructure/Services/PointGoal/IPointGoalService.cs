using UnityEngine.Events;

namespace Infrastructure.Services.PointGoal
{
    public interface IPointGoalService : IService
    {
        public UnityAction OnPointsGoal { get; set; }
        public void WarmUp();
    }
}
