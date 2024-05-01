using Core.SceneLoader;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayState : IState
    {
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public GameplayState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async Task Enter()
        {
            Debug.Log("Enter GameplayState");
        }

        public async Task Exit()
        {
            Debug.Log("Exit GameplayState");
        }
    }
}