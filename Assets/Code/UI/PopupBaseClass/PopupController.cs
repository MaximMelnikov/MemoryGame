using Core.Services.Input;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PopupController : UIWidgetController
{
    private readonly IInputService _inputService;
    private PopupView _popupView;

    public PopupController(
        IInputService inputService)
    {
        _inputService = inputService;
    }

    public override async Task Show()
    {
        _inputService.DisableInput();
        await _popupView.Show();
        _inputService.EnableInput();
    }

    public override async Task Hide()
    {
        _inputService.DisableInput();
        await _popupView.Hide();
        _inputService.EnableInput();
    }
}