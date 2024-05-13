using System;
using System.Threading.Tasks;

namespace Core.StateMachine
{
    public interface IStateMachine
    {
        public Action<IState> OnStateChange { get; set; }
        public IState CurrentState { get; }
        public void RegisterState<TState>(IState state, bool force = false) where TState : IState;
        public Task Enter<TState>(bool force = false) where TState : class, IState;
    }
}