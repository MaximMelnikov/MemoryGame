using Core.SceneLoader;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class BootstrapState : IState
    {
        private const string MenuLevelName = "Menu";
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public async Task Enter()
        {
            Debug.Log("Enter BootstrapState");
            //You can show loading screen here and init services
            _sceneLoader.Load(MenuLevelName);
        }

        public async Task Exit()
        {
            Debug.Log("Exit BootstrapState");
        }
    }
}