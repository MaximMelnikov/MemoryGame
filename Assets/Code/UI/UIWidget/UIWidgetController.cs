using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public abstract class UIWidgetController
{
    protected abstract string ViewAssetKey { get; }
    private GameObject _preloadedAsset;
    protected readonly DiContainer _diContainer;

    public UIWidgetController(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    protected async Task PreloadAsset<T>()
    {
        _preloadedAsset = await Addressables.LoadAssetAsync<GameObject>(ViewAssetKey).Task;
    }

    protected async Task<T> Instantiate<T>() where T : class
    {
        if (!_preloadedAsset)
        {
            await PreloadAsset<T>();
        }
        
        var widget = _diContainer.InstantiatePrefabForComponent<T>(_preloadedAsset);
        return widget;
    }

    public abstract Task ShowView();
    public abstract Task HideView(bool autoDestroy = true);
}