using System;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HudView : UIWidgetView
{
    public static string ViewAssetKey => "ui_hud_widget";

    private HudViewModel _hudViewModel;

    [SerializeField]
    private TextMeshProUGUI _timerText;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _pauseButton;

    private IDisposable _timeRemainingSubscription;

    [Inject]
    public void Construct(
        HudViewModel hudViewModel)
    {
        _hudViewModel = hudViewModel;
    }

    public override void Initialize()
    {
        Debug.Log("HudView Initialize");
        Show();

        _timeRemainingSubscription = _hudViewModel.gameController.Timer.TimeRemaining.SubscribeToText(_timerText).AddTo(this);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnRestartButtonClick()
    {
        _hudViewModel.LevelRestart();
    }

    private void OnPauseButtonClick()
    {
        _hudViewModel.OpenPauseMenu();
    }

    public override Task Show()
    {
        return null;
    }

    public override Task Hide(bool autoDestroy = true)
    {
        

        return null;
    }

    private void OnDestroy()
    {
        _timeRemainingSubscription.Dispose();
    }
}
