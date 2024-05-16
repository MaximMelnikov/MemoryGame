using Core.SceneLoader;
using UnityEngine;
using Zenject;

public class MenuController
{
    private const string GameplayLevelName = "Gameplay";
    [SerializeField]
    private OptionsController _optionsController;

    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void PlayButton()
    {
        _sceneLoader.Load(GameplayLevelName);
    }
}
