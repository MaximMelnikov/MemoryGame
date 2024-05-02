using Core.SceneLoader;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayState : IState
    {
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IFieldCreator _fieldCreator;
        private readonly GameController _gameController;

        public GameplayState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader,
            IFieldCreator fieldCreator,
            GameController gameController)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _fieldCreator = fieldCreator;
            _gameController = gameController;
        }

        public async Task Enter()
        {
            Debug.Log("Enter GameplayState");
            _fieldCreator.CreateField();
            _gameController.Start();
        }

        public async Task Exit()
        {
            Debug.Log("Exit GameplayState");
        }
    }
}