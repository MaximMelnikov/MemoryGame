using Core.Services.Input;
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuView : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private List<RectTransform> _tabs;
    [SerializeField]
    private List<Button> _buttons;
    private IInputService _inputService;
    private MenuController _menuController;
    private int _currentTabIndex;

    [Inject]
    private void Construct(
        IInputService inputService,
        MenuController menuController)
    {
        _inputService = inputService;
        _menuController = menuController;
    }

    private void Awake()
    {
        _playButton.onClick.AddListener(_menuController.PlayButton);
    }

    public void ChangeTab(int index)
    {
        ShowTab(index, true);
    }

    public async Task ShowTab(int index, bool animated)
    {
        if (index == _currentTabIndex)
        {
            return;
        }
        _tabs[index].gameObject.SetActive(true);
        _buttons[index].interactable = false;
        _buttons[_currentTabIndex].interactable = true;
        
        if (animated)
        {
            _inputService.DisableInput();
            float scrollSide = _currentTabIndex < index ? 1f : -1f;
            _tabs[index].DOAnchorPosX(scrollSide * Screen.width, 0);
            _tabs[index].DOAnchorPosX(0, .5f);
            await _tabs[_currentTabIndex].DOAnchorPosX(0 - scrollSide * Screen.width, .5f).AsyncWaitForCompletion();
            _inputService.EnableInput();
        }

        _tabs[_currentTabIndex].gameObject.SetActive(false);

        _currentTabIndex = index;
    }
}