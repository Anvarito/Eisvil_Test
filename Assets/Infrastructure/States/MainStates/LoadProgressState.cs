namespace Infrastructure.States.MainStates
{
    /*
    public class LoadProgressState : IState
    {
        private readonly MainStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadProgressService;
        private readonly IEconomyService _economyService;

        public LoadProgressState(
            MainStateMachine gameStateMachine, 
            IPersistentDataService progressService,
            ISaveLoadService saveLoadProgressService,
            IEconomyService economyService)
        {
            _stateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgressService = saveLoadProgressService;
            _economyService = economyService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            InitEconomyByProgress();

            //_stateMachine.Enter<LoadMetaState>();
            _stateMachine.Enter<LoadGameState>();
        }

        public void Exit()
        {
            
        }

        private async void LoadProgressOrInitNew()
        {
            _progressService.Settings = 
                await _saveLoadProgressService.LoadSettings() 
                ?? NewSettings();
            
            _progressService.Progress = 
                await _saveLoadProgressService.LoadProgress() 
                ?? NewProgress();
            
            _progressService.Economy = 
                await _saveLoadProgressService.LoadEconomy() 
                ?? NewEconomy();
        }

        private void InitEconomyByProgress()
        {
            _economyService.PlayerCurrency.Value = _progressService.Economy.PlayerCurrency;
            _economyService.PlayerCurrency
                .Subscribe(_ =>
                {
                    _progressService.Economy.PlayerCurrency = _economyService.PlayerCurrency.Value;
                    _saveLoadProgressService.SaveEconomy();
                });
        }

        #region Creation economy, progress and settings stubs

        private PlayerEconomyData NewEconomy() =>
            new()
            {
                PlayerCurrency = 100,
                InventoryItems = new Dictionary<string, int>()
            };

        private PlayerProgressData NewProgress() =>
            new()
            {
                CompletedStages = new HashSet<string>()
            };

        private PlayerSettingsData NewSettings() =>
            new()
            {
                MusicVolume = 100,
                SfxVolume = 100,
                DebugEnabled = false,
                HapticEnabled = true
            };

        #endregion
    }*/
}