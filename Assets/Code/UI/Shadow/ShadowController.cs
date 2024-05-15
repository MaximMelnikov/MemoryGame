using System.Threading.Tasks;
using UnityEngine;
using Zenject;

/// <summary>
/// Base class of all ui widgets controllers
/// </summary>
public class ShadowController : UIWidgetController
{
    protected override string ViewAssetKey => "ui_shadow_widget";
    private ShadowView _shadowView;

    public ShadowController(DiContainer diContainer) : base(diContainer)
    {
    }

    public override async Task HideView(bool autoDestroy = true)
    {
        await _shadowView.Hide();
    }

    public override async Task ShowView()
    {
        _shadowView = await Instantiate<ShadowView>();
        await _shadowView.Show();
    }
}