using Core.Services.Input;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _window;
    [SerializeField]
    private CanvasGroup _shadow;

    [SerializeField]
    private Toggle _soundToggle;
    [SerializeField]
    private Slider _difficultySlider;

    private IInputService _inputService;
    private OptionsService _optionsService;

    [Inject]
    private void Construct(
        IInputService inputService,
        OptionsService optionsService)
    {
        _inputService = inputService;
        _optionsService = optionsService;
    }

    private void Start()
    {
        SetOptionsValues();

        _soundToggle.onValueChanged.AddListener(OnSoundToggleValueChanged);
        _difficultySlider.onValueChanged.AddListener(OnDiffcultySliderValueChanged);
    }

    public void OnSaveButton()
    {
        Hide();
        _optionsService.Save();
    }

    public async Task Show()
    {
        //anim
        _shadow.DOFade(1, .2f);
        _shadow.blocksRaycasts = true;

        var rectTransform = _window.GetComponent<RectTransform>();
        _inputService.DisableInput();
        rectTransform.DOMoveY(100, 0);
        rectTransform.DOLocalJump(Vector3.zero, 1, 3, .3f);
        await _window.DOFade(1, .3f).AsyncWaitForCompletion();
        _window.blocksRaycasts = true;
        _window.interactable = true;
        _inputService.EnableInput();
    }

    private async Task Hide()
    {
        //anim
        _shadow.DOFade(0, .1f);
        _shadow.blocksRaycasts = false;

        var rectTransform = _window.GetComponent<RectTransform>();
        _inputService.DisableInput();
        rectTransform.DOScale(0, .1f);
        await _window.DOFade(0, .1f).AsyncWaitForCompletion();
        _window.blocksRaycasts = false;
        _window.interactable = false;
        rectTransform.DOScale(1, 0);
        _inputService.EnableInput();
    }

    private void SetOptionsValues()
    {
        _soundToggle.SetIsOnWithoutNotify(_optionsService.sound.Value);
        _difficultySlider.SetValueWithoutNotify(_optionsService.difficulty.Value);
    }

    private void OnSoundToggleValueChanged(bool value)
    {
        _optionsService.sound.Value = value;
    }

    private void OnDiffcultySliderValueChanged(float value)
    {
        _optionsService.difficulty.Value = (int)value;
    }
}
