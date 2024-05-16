using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinView : PopupView
{
    private WinController _winController;
    private GameController _gameController;

    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private ParticleSystem _firework;

    [Inject]
    private void Construct(
        WinController winController,
        GameController gameController)
    {
        _winController = winController;
        _gameController = gameController;
    }

    private new void Awake()
    {
        base.Awake();
        _playButton.onClick.AddListener(_winController.OnPlayButton);
    }

    public void OnPlayButton()
    {
        Hide();
    }

    public override async Task Show()
    {
        await base.Show();
    }
}