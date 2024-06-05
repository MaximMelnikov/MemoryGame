using Zenject;

public class OptionsController
{
    private OptionsService _optionsService;

    [Inject]
    private void Construct(
        OptionsService optionsService)
    {
        _optionsService = optionsService;
    }

    public bool GetSoundOptionValue()
    {
        return _optionsService.sound.Value;
    }

    public int GetdifficultyOptionValue()
    {
        return _optionsService.difficulty.Value;
    }

    public void OnSoundToggleValueChanged(bool value)
    {
        _optionsService.sound.Value = value;
        _optionsService.Save();
    }

    public void OnDiffcultySliderValueChanged(float value)
    {
        _optionsService.difficulty.Value = (int)value;
        _optionsService.Save();
    }
}
