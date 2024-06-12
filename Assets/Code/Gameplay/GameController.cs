using Core.Services.Input;
using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class GameController : IDisposable
{
    private readonly DiContainer _container;
    private FieldSettings _fieldSettings;
    private IFieldCreator _fieldCreator;
    private IInputService _inputService;

    private Card _selectedCard;
    private int _pairsFound;

    public CountdownTimer Timer { get; private set; }

    public GameController(
        DiContainer container,
        FieldSettings fieldSettings,
        IFieldCreator fieldCreator,
        IInputService inputService)
    {
        _container = container;
        _fieldSettings = fieldSettings;
        _fieldCreator = fieldCreator;
        _inputService = inputService;

        Timer = new CountdownTimer(_fieldSettings.timeToFail, Loose);
        Timer.Pause();
    }

    public async Task Play()
    {
        _inputService.DisableInput();
        await FlipAllCardsAnim();
        await Task.Delay(_fieldSettings.showCardsTime * 1000);
        await FlipAllCardsAnim();
        _inputService.EnableInput();
        Timer.StartNewTimer(_fieldSettings.timeToFail);
    }

    public async Task Restart()
    {
        if (!_inputService.IsEnabled)
        {
            return;
        }

        _inputService.DisableInput();

        //reset
        foreach (var item in _fieldCreator.Cards)
        {
            item.Reset();
        }

        _selectedCard = null;
        _pairsFound = 0;
        Timer.Pause();

        //shuffle cards
        _fieldCreator.ShuffleField();
        await Task.Delay(1000);
        Play();
    }

    public void OnCardSelected(Card card)
    {
        if (!_selectedCard)
        {
            _selectedCard = card;
            return;
        }

        if (_selectedCard.Id == card.Id)
        {
            _selectedCard.PairFound();
            card.PairFound();
            _pairsFound++;
        }
        else
        {
            _selectedCard.FailPair();
            card.FailPair();
        }
        _selectedCard = null;

        //check for win condition
        if (_pairsFound == _fieldSettings.rowsCount * _fieldSettings.columnsCount / 2)
        {
            Win();
        }
    }

    private void Win()
    {
        _container.OpenWindow<WinView, WinViewModel>(WinView.ViewAssetKey);
    }

    private void Loose()
    {
        _container.OpenWindow<LooseView, LooseViewModel>(LooseView.ViewAssetKey);
    }

    private async Task FlipAllCardsAnim()
    {
        foreach (var item in _fieldCreator.Cards)
        {
            item.FlipCard();
            await Task.Delay(50);
        }
    }

    public void Dispose()
    {
        Debug.Log("GameController Dispose");
        Timer.Dispose();
    }

    ~GameController()
    {
        Debug.Log("GameController DESTR");
    }
}