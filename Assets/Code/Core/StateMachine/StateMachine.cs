﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.StateMachine
{
    public class StateMachine : IStateMachine
    {
        public Action<IState> OnStateChange { get; set; }
        public IState CurrentState { get; private set; }
        private readonly Dictionary<Type, IState> _registeredStates = new Dictionary<Type, IState>();

        public void RegisterState<TState>(IState state, bool force = false) where TState : IState
        {
            if (_registeredStates.ContainsKey(typeof(TState)))
            {
                if (force)
                {
                    _registeredStates[typeof(TState)] = state;
                }
                return;
            }
            _registeredStates.Add(typeof(TState), state);
        }

        public async Task Enter<TState>(bool force = false) where TState : class, IState
        {
            if (!force && CurrentState == GetState<TState>())
                return;
            
            TState newState = await ChangeState<TState>();
            await newState.Enter();
        }

        private async Task<TState> ChangeState<TState>() where TState : class, IState
        {
            if (CurrentState != null)
                await CurrentState.Exit();
            
            TState newState = GetState<TState>();
            
            CurrentState = newState;
            OnStateChange?.Invoke(CurrentState);
            return newState;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _registeredStates[typeof(TState)] as TState;
        }
    }
}