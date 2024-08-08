using UnityEngine.Events;

namespace Infrastructure.Services.TimerServices
{
    public interface IStartTimerService
    {
        UnityAction OnTimerOut { get; set; }
        void Launch();
    }
}