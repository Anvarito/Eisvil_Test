using System;
using System.Collections.Generic;
using Infrastructure.Factories.StateFactories;
using Infrastructure.Services.Logging;
using Infrastructure.States.Interfaces;
using Infrastructure.States.MainStates;
using Zenject;

namespace Infrastructure.States.StateMachines
{
    public class MainStateMachine : StateMachineBase, IInitializable
    {
        private readonly MainStateFactory _mainStateFactory;
        
        public MainStateMachine(MainStateFactory mainStateFactory, ILoggingService loggingService) : base(loggingService)
        {
            _mainStateFactory = mainStateFactory;
        }
        
        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)]    = _mainStateFactory.CreateState<BootstrapState>(),
                [typeof(LoadGameState)]    = _mainStateFactory.CreateState<LoadGameState>(),
            };
            
            Enter<BootstrapState>();
        }

    }
}
