using Core.SceneLoader;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class MenuState : IState
    {
        private const string MenuLevelName = "Menu";
        private readonly ISceneLoader _sceneLoader;

        public MenuState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async Task Enter()
        {
            _sceneLoader.Load(MenuLevelName);
            Debug.Log("Enter MenuState");
        }

        public async Task Exit()
        {
            Debug.Log("Exit MenuState");
        }
    }
}