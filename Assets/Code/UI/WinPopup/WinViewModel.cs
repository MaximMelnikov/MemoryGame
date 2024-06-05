using Zenject;

public class WinViewModel : IViewModel
{
    private GameController _gameController;

    [Inject]
    public WinViewModel(
        GameController gameController)
    {
        _gameController = gameController;
    }

    public void OnPlayButton()
    {
        _gameController.Restart();
    }
}