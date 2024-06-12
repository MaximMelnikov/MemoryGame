using UnityEngine;
using Zenject;

public class LooseViewModel : IViewModel
{
    private GameController _gameController;

    [Inject]
    public LooseViewModel(
        GameController gameController)
    {
        _gameController = gameController;
    }

    public void OnPlayButton()
    {
        _gameController.Restart();
    }

    ~LooseViewModel()
    {
        Debug.Log("LooseViewModel destr");
    }
}