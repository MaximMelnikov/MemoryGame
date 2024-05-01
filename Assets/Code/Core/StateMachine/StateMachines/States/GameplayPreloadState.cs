using System.Threading.Tasks;
using Core.SceneLoader;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayPreloadState : IState
    {
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public GameplayPreloadState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async Task Enter()
        {
            Debug.Log("Enter GameplayPreloadState");
            _projectStateMachine.Enter<GameplayState>();
        }

        public async Task Exit()
        {
            Debug.Log("Exit GameplayPreloadState");
        }
    }
}