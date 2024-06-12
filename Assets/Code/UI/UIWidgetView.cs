using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class UIWidgetView : MonoBehaviour
{
    protected Canvas _canvas;

    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
        SetupCanvas();
    }

    private void SetupCanvas()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;
    }

    public abstract void Initialize();

    public abstract Task Show();

    public abstract Task Hide(bool autoDestroy = true);
}