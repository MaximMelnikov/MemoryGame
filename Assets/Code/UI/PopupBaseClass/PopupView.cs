using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class PopupView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _popup;
    private RectTransform _popupRectTransform;
    private Sequence _showSequence;
    private Sequence _hideSequence;

    private void Awake()
    {
        _popupRectTransform = _popup.GetComponent<RectTransform>();
        CreateShowSequence();
        CreateHideSequence();
    }

    public virtual async Task Show()
    {
        _showSequence.Restart();
        await _showSequence.AsyncWaitForCompletion();
    }

    public async Task Hide()
    {
        _hideSequence.Restart();
        await _hideSequence.AsyncWaitForCompletion();
    }

    protected virtual void CreateShowSequence()
    {
        _showSequence = DOTween.Sequence();
        _showSequence.SetAutoKill(false);
        _showSequence.Pause();

        _showSequence.Append(_popupRectTransform.DOMoveY(100, 0));
        _showSequence.Join(_popupRectTransform.DOLocalJump(Vector3.zero, 1, 3, .3f));
        _showSequence.Join(_popup.DOFade(1, .3f));
    }

    protected virtual void CreateHideSequence()
    {
        _hideSequence = DOTween.Sequence();
        _hideSequence.SetAutoKill(false);
        _hideSequence.Pause();

        _hideSequence.Append(_popup.DOFade(0, .1f));
        _hideSequence.Join(_popupRectTransform.DOScale(1, 0));
    }
}