using Core.Services.Input;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Lean.Touch;
using UnityEngine;

public abstract class Card : MonoBehaviour, IInputInteractable
{
    [SerializeField]
    private SpriteRenderer _cardBack;

    protected GameController _gameController;
    protected AudioService _audioService;

    protected const float _flipTime = .2f;
    protected Sequence _flipSequence;
    protected Sequence _failSequence;

    public int Id { get; protected set; }
    public bool IsFaced { get; protected set; }
    public bool IsInputEnabled { get; set; }

    public abstract void Init(int id);

    public void Reset()
    {
        _flipSequence.Complete();
        _failSequence.Complete();
        if (IsFaced)
        {
            FlipCard();
        }
    }

    public void InputAction(LeanFinger finger)
    {
        if (IsFaced)
        {
            return;
        }
        OnSelected();
    }

    protected async UniTask OnSelected()
    {
        _audioService.PlayClick();
        await FlipCard().AsyncWaitForCompletion();
        _gameController.OnCardSelected(this);
    }

    public Sequence FlipCard()
    {
        IsInputEnabled = false;
        _flipSequence = DOTween.Sequence();
        _flipSequence
            .Append(_cardBack.transform.DORotate(new Vector3(0, IsFaced ? 0 : 180, 0), _flipTime*2))
            .AppendCallback(() =>
            {
                IsFaced = !IsFaced;
                IsInputEnabled = true;
            });
        return _flipSequence;
    }

    public Sequence FailPair()
    {
        _audioService.PlayFail();
        _failSequence = DOTween.Sequence();
        _failSequence
            .Append(_cardBack.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo))
            .AppendInterval(1)
            .Append(FlipCard());
        return _failSequence;
    }
    public void PairFound()
    {
        _audioService.PlaySuccess();
    }
}