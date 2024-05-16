using Core.Services.Input;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class WinController : UIWidgetController
{
    protected override string ViewAssetKey => "ui_win_popup";

    private AudioService _audioService;
    private IInputService _inputService;
    private GameController _gameController;

    private WinView _winView;

    [Inject]
    public WinController(
        DiContainer diContainer,
        IInputService inputService,
        AudioService audioService,
        GameController gameController) : base (diContainer)
    {
        _audioService = audioService;
        _inputService = inputService;
        _gameController = gameController;

        Start();
    }

    private void Start()
    {
        _gameController.OnWin += OnWin;
    }

    public void OnPlayButton()
    {
        _gameController.Restart();
        HideView();
    }

    public override async Task ShowView()
    {
        _inputService.DisableInput();
        if (!_winView)
        {
            _winView = await Instantiate<WinView>();
        }
        await _winView.Show();
        _audioService.PlayWin();
        _inputService.EnableInput();
    }

    public override async Task HideView(bool autoDestroy = true)
    {
        await _winView.Hide();
        if (autoDestroy)
        {
            GameObject.Destroy(_winView);
        }

        _audioService.PlayClick();
    }

    private void OnWin()
    {
        ShowView();
    }

    private void OnDestroy()
    {
        _gameController.OnWin -= OnWin;
    }
}
