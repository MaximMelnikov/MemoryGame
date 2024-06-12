using Core.Services.Input;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LooseView : UIPopupView
{
    public static string ViewAssetKey => "ui_loose_popup";

    private LooseViewModel _looseViewModel;
    private AudioService _audioService;
    private IInputService _inputService;

    [SerializeField]
    private Button _playButton;

    [Inject]
    public void Construct(
        LooseViewModel looseViewModel,
        IInputService inputService,
        AudioService audioService)
    {
        base.Construct(inputService);
        _looseViewModel = looseViewModel;
        _audioService = audioService;
        _inputService = inputService;
    }

    public override void Initialize()
    {
        Show();
        _playButton.onClick.AddListener(OnPlayButton);
    }

    public override async Task Show()
    {
        _inputService.DisableInput();
        await base.Show();
        _audioService.PlayLoose();
    }

    public override async Task Hide(bool autoDestroy = true)
    {
        await base.Hide();
        _audioService.PlayClick();
        _inputService.EnableInput();
    }

    private void OnPlayButton()
    {
        _looseViewModel.OnPlayButton();
        Hide();
    }
}