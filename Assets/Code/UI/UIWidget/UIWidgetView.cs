using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Base class of all ui widgets views
/// </summary>
[RequireComponent(typeof(Canvas))]
public abstract class UIWidgetView : MonoBehaviour
{
    protected Canvas _canvas;

    protected void Awake()
    {
        _canvas = GetComponent<Canvas>();
        SetupCanvas();
    }

    private void SetupCanvas()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;
    }

    public abstract Task Show();
    public abstract Task Hide();
}