using Core.Services.Input;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _window;
    [SerializeField]
    private CanvasGroup _shadow;
    private AudioService _audioService;
    private IInputService _inputService;
    private GameController _gameController;

    [Inject]
    private void Construct(
        IInputService inputService,
        AudioService audioService,
        GameController gameController)
    {
        _audioService = audioService;
        _inputService = inputService;
        _gameController = gameController;

        _gameController.OnWin += OnWin;
    }

    public void OnPlayButton()
    {
        Hide();
    }

    private async Task Show()
    {
        var rectTransform = _window.GetComponent<RectTransform>();

        //anim
        _shadow.DOFade(1, .2f);
        _shadow.blocksRaycasts = true;

        _inputService.DisableInput();
        rectTransform.DOMoveY(100, 0);
        rectTransform.DOLocalJump(Vector3.zero, 1, 3, .3f);
        await _window.DOFade(1, .3f).AsyncWaitForCompletion();
        _window.blocksRaycasts = true;
        _window.interactable = true;
        _audioService.PlayWin();
        _inputService.EnableInput();
    }

    private async Task Hide()
    {
        _gameController.Hint();

        //anim
        _shadow.DOFade(0, .1f);
        _shadow.blocksRaycasts = false;

        var rectTransform = _window.GetComponent<RectTransform>();
        _inputService.DisableInput();
        _audioService.PlayClick();
        rectTransform.DOScale(0, .1f);
        await _window.DOFade(0, .1f).AsyncWaitForCompletion();
        _window.blocksRaycasts = false;
        _window.interactable = false;
        rectTransform.DOScale(1, 0);
        _inputService.EnableInput();
    }

    private void OnWin()
    {
        Show();
    }

    private void OnDestroy()
    {
        _gameController.OnWin -= OnWin;
    }
}
