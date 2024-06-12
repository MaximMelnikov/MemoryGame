using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayState : IState
    {
        private IFieldCreator _fieldCreator;
        private GameController _gameController;

        public GameplayState(
            IFieldCreator fieldCreator,
            GameController gameController)
        {
            _fieldCreator = fieldCreator;
            _gameController = gameController;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter GameplayState");
            _fieldCreator.CreateField();
            _gameController.Play();
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit GameplayState");
            
            _fieldCreator.Dispose();
            _gameController.Dispose();
        }
    }
}