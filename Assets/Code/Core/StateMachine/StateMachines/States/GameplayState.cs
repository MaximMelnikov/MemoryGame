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

        public GameplayState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader,
            IFieldCreator fieldCreator)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _fieldCreator = fieldCreator;
        }

        public async Task Enter()
        {
            Debug.Log("Enter GameplayState");
            _fieldCreator.CreateField();

        }

        public async Task Exit()
        {
            Debug.Log("Exit GameplayState");
        }
    }
}