using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayPreloadState : IState
    {
        private readonly DiContainer _container;
        private readonly IStateMachine _projectStateMachine;

        public GameplayPreloadState(
            DiContainer container,
            IStateMachine projectStateMachine)
        {
            _container = container;
            _projectStateMachine = projectStateMachine;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter GameplayPreloadState");
            await _container.OpenWindow<HudView, HudViewModel>(HudView.ViewAssetKey);
            _projectStateMachine.Enter<GameplayState>();
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit GameplayPreloadState");
        }
    }
}