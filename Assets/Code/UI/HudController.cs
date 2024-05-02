using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private GameController _gameController;

    [Inject]
    private void Construct(GameController gameController)
    {
        _gameController = gameController;
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

    private void OnDestroy()
    {
        _gameController.OnTimerTick -= OnTimerValueChange;
    }
}