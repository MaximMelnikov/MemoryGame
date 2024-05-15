using Core.Services.Input;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class LooseController : UIWidgetController
{
    protected override string ViewAssetKey => "ui_loose_popup";

    private AudioService _audioService;
    private GameController _gameController;
    private IInputService _inputService;
    private LooseView _looseView;

    public LooseController(
        DiContainer diContainer,
        IInputService inputService,
        AudioService audioService,
        GameController gameController) : base(diContainer)
    {
        _inputService = inputService;
        _audioService = audioService;
        _gameController = gameController;
    }

    public void OnPlayButton()
    {
        _gameController.Restart();
        HideView(true);
    }

    public override async Task ShowView()
    {
        _inputService.DisableInput();
        if (!_looseView)
        {
            _looseView = await Instantiate<LooseView>();
        }
        await _looseView.Show();
        _inputService.EnableInput();
        _audioService.PlayLoose();
    }

    public override async Task HideView(bool autoDestroy = true)
    {
        await _looseView.Hide();
        if (autoDestroy)
        {
            GameObject.Destroy(_looseView);
        }
        
        _audioService.PlayClick();
    }
}