using System.Threading.Tasks;
using UnityEngine;

public abstract class UIWidgetController
{
    protected async Task PreloadAsset()
    {

    }

    protected async Task Instantiate()
    {

    }

    public abstract Task Show();
    public abstract Task Hide();
}