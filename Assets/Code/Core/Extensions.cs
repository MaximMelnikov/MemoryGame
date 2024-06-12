using System;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public static class Extensions
{
    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        // Get the bottom left corner
        Vector3 position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        return new Rect(position, size);
    }

    public static IDisposable SubscribeToText(this IObservable<string> source, TextMeshProUGUI text)
    {
        return source.SubscribeWithState(text, (x, t) => t.text = x);
    }

    public static IDisposable SubscribeToText<T>(this IObservable<T> source, TextMeshProUGUI text)
    {
        return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
    }

    public static async Task<TView> OpenWindow<TView, TViewModel>(this DiContainer container, string viewAssetKey, string uid = null) where TView : UIWidgetView where TViewModel : IViewModel
    {
        GameObject gameObject = null;
        uid = uid ?? viewAssetKey;
        try
        {
            gameObject = await Addressables.LoadAssetAsync<GameObject>(uid).Task;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"{typeof(TView)} view can't be loaded. ViewAssetKey: {viewAssetKey}");
            throw e;
        }
        var instantiatedGameObject = GameObject.Instantiate(gameObject);
        var uiWidgetView = container.InjectGameObjectForComponent<TView>(instantiatedGameObject);
        uiWidgetView.Initialize();

        return uiWidgetView;
    }
}