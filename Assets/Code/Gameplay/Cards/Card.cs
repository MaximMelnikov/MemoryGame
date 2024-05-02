using Core.Services.Input;
using DG.Tweening;
using Lean.Touch;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Card : MonoBehaviour, IInputInteractable
{
    private CardTilesDatabase _cardTilesDatabase;
    private GameController _gameController;
    [SerializeField]
    private float _flipTime = .2f;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Sequence flipSequence;
    private Sequence failSequence;

    public int Id { get; private set; }
    public bool IsFaced { get; private set; }
    public bool IsInputEnabled { get; set; }

    [Inject]
    private void Construct(
        CardTilesDatabase cardTilesDatabase,
        GameController gameController
        )
    {
        _cardTilesDatabase = cardTilesDatabase;
        _gameController = gameController;
    }

    public void Init(int id)
    {
        IsInputEnabled = true;
        Id = id;
    }

    private void SetSprite(int id)
    {
        _spriteRenderer.sprite = _cardTilesDatabase.GetSprite(id);
    }

    public Sequence FlipCard()
    {
        flipSequence = DOTween.Sequence();
        flipSequence
            .AppendCallback(() => IsInputEnabled = false)
            .Append(_spriteRenderer.transform.DORotate(new Vector3(0, 90, 0), _flipTime))
            .AppendCallback(() => SetSprite(IsFaced ? FieldSettings.backCardId : Id))
            .Append(_spriteRenderer.transform.DORotate(new Vector3(0, 180, 0), _flipTime))
            .AppendCallback(() =>
            {
                IsFaced = !IsFaced;
                IsInputEnabled = true;
            });
        return flipSequence;
    }

    public void Reset()
    {
        flipSequence.Complete();
        failSequence.Complete();
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

    private async Task OnSelected()
    {
        await FlipCard().AsyncWaitForCompletion();
        _gameController.OnCardSelected(this);
    }

    public async Task PairFound()
    {

    }

    public Sequence FailPair()
    {
        failSequence = DOTween.Sequence();
        failSequence
            .Append(_spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo))
            .AppendInterval(1)
            .Append(FlipCard());
        return failSequence;
    }
}
