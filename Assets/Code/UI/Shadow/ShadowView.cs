using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class ShadowView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _shadow;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _shadow.alpha = 0f;
    }

    protected virtual async Task ShowShadow(int sortingOrder)
    {
        _canvas.sortingOrder = sortingOrder;
        await _shadow.DOFade(1, .2f).AsyncWaitForCompletion();
    }

    protected virtual async Task HideShadow()
    {
        _shadow.DOFade(0, .2f);
    }
}