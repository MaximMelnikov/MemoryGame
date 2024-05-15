using Core.SceneLoader;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class HudController : UIWidgetController
{
    public Action<int> OnTimerValueChange;
    private GameController _gameController;
    private ISceneLoader _sceneLoader;
    private HudView _hudView;

    [Inject]
    public HudController(
        DiContainer diContainer,
        GameController gameController,
        ISceneLoader sceneLoader) : base(diContainer)
    {
        _gameController = gameController;
        _sceneLoader = sceneLoader;

        Start();
    }

    protected override string ViewAssetKey => "ui_hud_widget";

    private void Start()
    {
        _gameController.OnTimerTick += ChangeTimerValue;
        ChangeTimerValue(60);
    }

    private void ChangeTimerValue(int seconds)
    {
        OnTimerValueChange?.Invoke(seconds);
    }

    public void LevelRestart()
    {
        _gameController.Restart();
    }

    public void OpenPauseMenu()
    {
        _sceneLoader.Load("Menu");
    }

    private void OnDestroy()
    {
        _gameController.OnTimerTick -= OnTimerValueChange;
    }

    public override async Task ShowView()
    {
        if (!_hudView)
        {
            _hudView = await Instantiate<HudView>();
        }
        await _hudView.Show();
    }

    public override async Task HideView(bool autoDestroy = true)
    {
        await _hudView.Hide();
        if (autoDestroy)
        {
            GameObject.Destroy(_hudView);
        }
    }
}