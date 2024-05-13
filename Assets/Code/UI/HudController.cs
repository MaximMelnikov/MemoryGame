using Core.SceneLoader;
using TMPro;
using UnityEngine;
using Zenject;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private GameController _gameController;
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(
        GameController gameController,
        ISceneLoader sceneLoader)
    {
        _gameController = gameController;
        _sceneLoader = sceneLoader;
        _gameController.OnTimerTick += OnTimerValueChange;
        OnTimerValueChange(60);
    }

    private void OnTimerValueChange(int seconds)
    {
        timerText.text = seconds.ToString();
    }

    public void OnHintButtonClick()
    {
        _gameController.Hint();
    }

    public void OnMenuButtonClick()
    {
        _sceneLoader.Load("Menu");
    }

    private void OnDestroy()
    {
        _gameController.OnTimerTick -= OnTimerValueChange;
    }
}