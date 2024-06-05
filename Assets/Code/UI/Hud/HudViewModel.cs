using Core.SceneLoader;
using System;
using Zenject;

public class HudViewModel : IViewModel
{
    public GameController gameController;
    private ISceneLoader _sceneLoader;

    [Inject]
    public HudViewModel(
        GameController gameController,
        ISceneLoader sceneLoader)
    {
        this.gameController = gameController;
        _sceneLoader = sceneLoader;
    }

    public void LevelRestart()
    {
        this.gameController.Restart();
    }

    public void OpenPauseMenu()
    {
        _sceneLoader.Load("Menu");
    }
}