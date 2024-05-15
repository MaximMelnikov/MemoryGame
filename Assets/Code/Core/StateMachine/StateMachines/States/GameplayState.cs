using Core.SceneLoader;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayState : IState
    {
        private readonly DiContainer _container;
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IFieldCreator _fieldCreator;
        private readonly GameController _gameController;
        private readonly HudController _hudController;

        public GameplayState(
            DiContainer container,
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader,
            IFieldCreator fieldCreator,
            GameController gameController,
            HudController hudController)
        {
            _container = container;
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _fieldCreator = fieldCreator;
            _gameController = gameController;
            _hudController = hudController;
        }

        public async Task Enter()
        {
            Debug.Log("Enter GameplayState");
            _fieldCreator.CreateField();
            _gameController.Play();
        }

        public async Task Exit()
        {
            await _hudController.HideView();
            Debug.Log("Exit GameplayState");
        }
    }
}