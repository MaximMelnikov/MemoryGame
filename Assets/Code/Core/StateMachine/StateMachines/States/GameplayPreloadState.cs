using System.ComponentModel;
using System.Threading.Tasks;
using Core.SceneLoader;
using UnityEngine;
using Zenject;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayPreloadState : IState
    {
        private readonly DiContainer _container;
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly HudController _hudController;

        public GameplayPreloadState(
            DiContainer container,
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader,
            HudController hudController)
        {
            _container = container;
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _hudController = hudController;
        }

        public async Task Enter()
        {
            Debug.Log("Enter GameplayPreloadState");

            await _hudController.ShowView();
            _projectStateMachine.Enter<GameplayState>();
        }

        public async Task Exit()
        {
            Debug.Log("Exit GameplayPreloadState");
        }
    }
}