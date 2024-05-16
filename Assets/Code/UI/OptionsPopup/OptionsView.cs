using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OptionsView : MonoBehaviour
{
    [SerializeField]
    private Toggle _soundToggle;
    [SerializeField]
    private Slider _difficultySlider;
    private OptionsController _optionsController;

    [Inject]
    private void Construct(
        OptionsController optionsController)
    {
        _optionsController = optionsController;
    }

    private void Awake()
    {
        _soundToggle.onValueChanged.AddListener(_optionsController.OnSoundToggleValueChanged);
        _difficultySlider.onValueChanged.AddListener(_optionsController.OnDiffcultySliderValueChanged);
        SetOptionsValues();
    }

    public void SetOptionsValues()
    {
        _soundToggle.SetIsOnWithoutNotify(_optionsController.GetSoundOptionValue());
        _difficultySlider.SetValueWithoutNotify(_optionsController.GetdifficultyOptionValue());
    }
}