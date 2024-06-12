using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class ShadowView : UIWidgetView
{
    private const int DefaultSorting = 5;
    [SerializeField]
    private CanvasGroup _shadow;

    public override void Initialize()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        _shadow.alpha = 0f;
    }

    public override async Task Show()
    {
        await Show(DefaultSorting);
    }

    public async Task Show(int sortingOrder)
    {
        _canvas.sortingOrder = sortingOrder;
        await _shadow.DOFade(1, .2f).AsyncWaitForCompletion();
    }

    public override async Task Hide(bool autoDestroy = true)
    {
        await _shadow.DOFade(0, .2f).AsyncWaitForCompletion();
    }
}