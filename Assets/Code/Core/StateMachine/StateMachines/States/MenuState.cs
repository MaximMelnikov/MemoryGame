using Core.SceneLoader;
using Cysharp.Threading.Tasks;
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

        public async UniTask Enter()
        {
            await _sceneLoader.Load(MenuLevelName);
            Debug.Log("Enter MenuState");
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit MenuState");
        }
    }
}