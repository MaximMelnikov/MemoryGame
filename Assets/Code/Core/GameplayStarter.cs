using Core.Factory;
using Core.SceneLoader;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameplayStarter : MonoBehaviour
    {
        private IStateMachine _projectStateMachine;
        private BootstrapperFactory _bootstrapperFactory;
        private ISceneLoader _sceneLoader;
        private IFieldCreator _fieldCreator;

        [Inject]
        private void Construct(
            IStateMachine projectStateMachine, 
            BootstrapperFactory bootstrapperFactory,
            ISceneLoader sceneLoader,
            IFieldCreator fieldCreator)
        {
            _projectStateMachine = projectStateMachine;
            _bootstrapperFactory = bootstrapperFactory;
            _sceneLoader = sceneLoader;
            _fieldCreator = fieldCreator;
        }
        
        private void Awake()
        {
            var projectStarter = FindObjectOfType<Bootstrapper>();
      
            if(projectStarter != null) return;

            _bootstrapperFactory.CreateBootstrapper();
        }

        private void Start()
        {
            _projectStateMachine.RegisterState<GameplayPreloadState>(new GameplayPreloadState(_projectStateMachine, _sceneLoader));
            _projectStateMachine.RegisterState<GameplayState>(new GameplayState(_projectStateMachine, _sceneLoader, _fieldCreator));
            _projectStateMachine.Enter<GameplayPreloadState>();
        }
    }
}