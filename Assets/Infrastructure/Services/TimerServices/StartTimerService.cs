using Infrastructure.Constants;
using Infrastructure.Services.Assets;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Infrastructure.Services.TimerServices
{
    public class StartTimerService : ITickable, IStartTimerService
    {
        public UnityAction OnTimerOut { get; set; }
        
        private readonly IAssetLoader _assetLoader;
        private  StartTimer _startTimer;
        private bool _isLaunch = false;
        private float _time = 3;
        
        public StartTimerService(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public void Tick()
        {
            if (_isLaunch)
            {
                _time -= Time.deltaTime;
                _startTimer.SetTimer(Mathf.RoundToInt(_time));
                if (_time <= 0)
                {
                    _isLaunch = false;
                    OnTimerOut?.Invoke();
                }
            }
        }

        public void Launch()
        {
            _startTimer = _assetLoader.Instantiate<StartTimer>(AssetPaths.StartTimer);
            _isLaunch = true;
        }
    }
}