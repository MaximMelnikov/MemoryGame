using Core.Services.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameController
{
    public event Action<int> OnTimerTick;

    private FieldSettings _fieldSettings;
    private IFieldCreator _fieldCreator;
    private IInputService _inputService;

    private Card _selectedCard;
    private int _pairsFound;

    private Timer _timer;
    private int _secondsToLoose;

    public GameController(
        FieldSettings fieldSettings,
        IFieldCreator fieldCreator,
        IInputService inputService)
    {
        _fieldSettings = fieldSettings;
        _fieldCreator = fieldCreator;
        _inputService = inputService;
    }

    public async Task Start()
    {
        _inputService.DisableInput();
        await FlipAllCardsAnim();
        await Task.Delay(_fieldSettings.showCardsTime * 1000);
        await FlipAllCardsAnim();
        _inputService.EnableInput();

        //start timer
        _secondsToLoose = _fieldSettings.timeToFail;
        _timer = Timer.Register(1, () => TimerTick(), isLooped: true);
        OnTimerTick.Invoke(_fieldSettings.timeToFail);
    }

    public async Task Hint()
    {
        if (!_inputService.IsEnabled)
        {
            return;
        }

        //reset
        foreach (var item in _fieldCreator.Cards)
        {
            item.Reset();
        }
        
        _selectedCard = null;
        _pairsFound = 0;
        _timer.Pause();

        //shuffle cards
        _fieldCreator.ShuffleField();
        await Task.Delay(1000);
        Start();
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
        _timer.Cancel();
        Debug.Log("Win");
    }

    private void Loose()
    {
        _timer.Cancel();
        Debug.Log("Loose");
    }

    private async Task FlipAllCardsAnim()
    {
        foreach (var item in _fieldCreator.Cards)
        {
            item.FlipCard();
            await Task.Delay(50);
        }
    }

    private void TimerTick()
    {
        _secondsToLoose--;
        OnTimerTick.Invoke(_secondsToLoose);

        if (_secondsToLoose <= 0)
        {
            Loose();
        }
    }
}
