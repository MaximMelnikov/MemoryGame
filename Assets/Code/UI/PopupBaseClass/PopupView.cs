using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class PopupView : UIWidgetView
{
    [SerializeField]
    private RectTransform _rootRectTransform;
    private CanvasGroup _rootCanvasGroup;
    
    private Sequence _showSequence;
    private Sequence _hideSequence;

    protected new void Awake()
    {
        base.Awake();

        _rootCanvasGroup = _rootRectTransform.GetComponent<CanvasGroup>();
        _rootCanvasGroup.alpha = 0f;
        CreateShowSequence();
        CreateHideSequence();
    }

    public override async Task Show()
    {
        _showSequence.Restart();
        await _showSequence.AsyncWaitForCompletion();
    }

    public override async Task Hide()
    {
        _hideSequence.Restart();
        await _hideSequence.AsyncWaitForCompletion();
    }

    protected virtual void CreateShowSequence()
    {
        _showSequence = DOTween.Sequence();
        _showSequence.SetAutoKill(false);
        _showSequence.Pause();

        _showSequence.Append(_rootRectTransform.DOLocalMoveY(100, 0));
        _showSequence.Append(_rootRectTransform.DOLocalMoveY(0, .3f));
        _showSequence.Join(_rootCanvasGroup.DOFade(1, .3f));
    }

    protected virtual void CreateHideSequence()
    {
        _hideSequence = DOTween.Sequence();
        _hideSequence.SetAutoKill(false);
        _hideSequence.Pause();

        _hideSequence.Append(_rootCanvasGroup.DOFade(0, .1f));
        _hideSequence.Join(_rootRectTransform.DOScale(1, 0));
    }
}