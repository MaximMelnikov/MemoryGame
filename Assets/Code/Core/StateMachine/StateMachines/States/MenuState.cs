using Core.SceneLoader;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class MenuState : IState
    {
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public MenuState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
        }

        public async Task Enter()
        {
            Debug.Log("Enter MenuState");
        }

        public async Task Exit()
        {
            Debug.Log("Exit MenuState");
        }
    }
}