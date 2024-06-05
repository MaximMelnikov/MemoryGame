using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

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

    public override void Initialize(IViewModel viewModel)
    {
        Debug.Log("HudView Initialize");
        Show();
        _hudViewModel = viewModel as HudViewModel;

        _hudViewModel.gameController.Timer.TimeRemaining.SubscribeToText(_timerText).AddTo(this);
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
}
