using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LooseView : PopupView
{
    private LooseController _looseController;

    [SerializeField]
    private Button _playButton;

    [Inject]
    private void Construct(LooseController looseController)
    {
        _looseController = looseController;
    }

    private new void Awake()
    {
        base.Awake();
        _playButton.onClick.AddListener(_looseController.OnPlayButton);
    }
}