using Core.Services.Input;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinView : UIPopupView
{
    public static string ViewAssetKey => "ui_win_popup";

    private WinViewModel _winController;
    private AudioService _audioService;
    private IInputService _inputService;

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private ParticleSystem _firework;

    [Inject]
    private void Construct(
        IInputService inputService,
        AudioService audioService)
    {
        _inputService = inputService;
        _audioService = audioService;
    }

    public override void Initialize()
    {
        Show();
        _playButton.onClick.AddListener(_winController.OnPlayButton);
    }

    public override async Task Show()
    {
        _inputService.DisableInput();
        await base.Show();
        _audioService.PlayWin();
    }

    public override async Task Hide(bool autoDestroy = true)
    {
        await base.Hide();
        _audioService.PlayClick();
        _inputService.EnableInput();
    }
}