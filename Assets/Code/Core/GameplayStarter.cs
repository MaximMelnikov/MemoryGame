using Core.Factory;
using Core.SceneLoader;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameplayStarter : MonoBehaviour
    {
        private DiContainer _container;
        private IStateMachine _projectStateMachine;
        private BootstrapperFactory _bootstrapperFactory;
        private ISceneLoader _sceneLoader;
        private IFieldCreator _fieldCreator;
        private GameController _gameController;

        [Inject]
        private void Construct(
            DiContainer container,
            IStateMachine projectStateMachine, 
            BootstrapperFactory bootstrapperFactory,
            ISceneLoader sceneLoader,
            IFieldCreator fieldCreator,
            GameController gameController)
        {
            _container = container;
            _projectStateMachine = projectStateMachine;
            _bootstrapperFactory = bootstrapperFactory;
        }
        
        private void Awake()
        {
            var projectStarter = FindObjectOfType<Bootstrapper>();
      
            if(projectStarter != null) return;

            _bootstrapperFactory.CreateBootstrapper();
        }

        private void Start()
        {
            _container.Bind<GameplayPreloadState>().AsSingle().NonLazy();
            var gameplayPreloadState = _container.Resolve<GameplayPreloadState>();
            _projectStateMachine.RegisterState<GameplayPreloadState>(gameplayPreloadState);

            _container.Bind<GameplayState>().AsSingle().NonLazy();
            var gameplayState = _container.Resolve<GameplayState>();
            _projectStateMachine.RegisterState<GameplayState>(gameplayState, true);

            _projectStateMachine.Enter<GameplayPreloadState>();
        }
    }
}