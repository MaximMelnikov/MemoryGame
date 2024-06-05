using Core.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.StateMachine.StateMachines.States
{
    public class BootstrapState : IState
    {
        private const string MenuLevelName = "Menu";
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _projectStateMachine;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter BootstrapState");
            //You can show loading screen here and init services
            Application.targetFrameRate = 60;
            if (SceneManager.GetActiveScene().name == "Start")
            {
                await _sceneLoader.Load(MenuLevelName);
            }
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit BootstrapState");
        }
    }
}