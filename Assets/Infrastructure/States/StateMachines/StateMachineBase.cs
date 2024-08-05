using System;
using System.Collections.Generic;
using Infrastructure.Services.Logging;
using Infrastructure.States.Interfaces;

namespace Infrastructure.States.StateMachines
{
    public abstract class StateMachineBase
    {
        protected Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        private readonly ILoggingService _logger;

        public StateMachineBase(ILoggingService loggingService)
        {
            _logger = loggingService;
        }
        
        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>()
                .Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>()
                .Enter(payload);


        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;
            
            _logger.LogMessage($"state changed to {_currentState.GetType().Name}", this);

            return state;
        }
    }
}
