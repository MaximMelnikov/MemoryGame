using Core.Factory;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameplayStarter : MonoBehaviour
    {
        private DiContainer _container;
        private IStateMachine _projectStateMachine;
        private BootstrapperFactory _bootstrapperFactory;

        [Inject]
        private void Construct(
            DiContainer container,
            IStateMachine projectStateMachine,
            BootstrapperFactory bootstrapperFactory)
        {
            _container = container;
            _projectStateMachine = projectStateMachine;
            _bootstrapperFactory = bootstrapperFactory;
        }

        private void Awake()
        {
            var projectStarter = FindObjectOfType<Bootstrapper>();

            if (projectStarter != null) return;

            _bootstrapperFactory.CreateBootstrapper();
        }

        private void Start()
        {
            Debug.Log("GameplayStarter_" + _container.GetHashCode());
            var gameplayPreloadState = _container.Resolve<GameplayPreloadState>();
            _projectStateMachine.RegisterState<GameplayPreloadState>(gameplayPreloadState);

            var gameplayState = _container.Resolve<GameplayState>();
            _projectStateMachine.RegisterState<GameplayState>(gameplayState);

            _projectStateMachine.Enter<GameplayPreloadState>();
        }
    }
}