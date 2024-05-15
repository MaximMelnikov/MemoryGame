using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HudView : UIWidgetView
{
    private HudController _hudController;

    [SerializeField]
    private TextMeshProUGUI _timerText;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _pauseButton;

    [Inject]
    private void Construct(
        HudController hudController)
    {
        _hudController = hudController;
    }

    protected void Awake()
    {
        base.Awake();

        _hudController.OnTimerValueChange += OnTimerValueChange;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnTimerValueChange(int seconds)
    {
        _timerText.text = seconds.ToString();
    }

    private void OnRestartButtonClick()
    {
        _hudController.LevelRestart();
    }

    private void OnPauseButtonClick()
    {
        _hudController.OpenPauseMenu();
    }

    public override async Task Show()
    {
    }

    public override async Task Hide()
    {
    }

    private void OnDestroy()
    {
        _hudController.OnTimerValueChange -= OnTimerValueChange;
    }
}
