using Core.Services.Input;
using DG.Tweening;
using Lean.Touch;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Card : MonoBehaviour, IInputInteractable
{
    private GameController _gameController;
    private AudioService _audioService;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private const float _flipTime = .2f;
    private Sequence _flipSequence;
    private Sequence _failSequence;
    private Sprite _facedSprite;
    private Sprite _backSprite;

    public int Id { get; private set; }
    public bool IsFaced { get; private set; }
    public bool IsInputEnabled { get; set; }

    [Inject]
    private void Construct(
        GameController gameController,
        AudioService audioService
        )
    {
        _gameController = gameController;
        _audioService = audioService;
    }

    public void Init(int id, Sprite facedSprite, Sprite backSprite)
    {
        Id = id;
        _facedSprite = facedSprite;
        _backSprite = backSprite;
    }

    private void SetSprite(bool isFaced)
    {
        _spriteRenderer.sprite = isFaced ? _facedSprite : _backSprite;
    }

    public Sequence FlipCard()
    {
        IsInputEnabled = false;        
        _flipSequence = DOTween.Sequence();
        _flipSequence
            .Append(_spriteRenderer.transform.DORotate(new Vector3(0, 90, 0), _flipTime))
            .AppendCallback(() => SetSprite(!IsFaced))
            .Append(_spriteRenderer.transform.DORotate(new Vector3(0, 180, 0), _flipTime))
            .AppendCallback(() =>
            {
                IsFaced = !IsFaced;
                IsInputEnabled = true;
            });
        return _flipSequence;
    }

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

    private async Task OnSelected()
    {
        _audioService.PlayClick();
        await FlipCard().AsyncWaitForCompletion();
        _gameController.OnCardSelected(this);
    }

    public async Task PairFound()
    {
        _audioService.PlaySuccess();
    }

    public Sequence FailPair()
    {
        _audioService.PlayFail();
        _failSequence = DOTween.Sequence();
        _failSequence
            .Append(_spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo))
            .AppendInterval(1)
            .Append(FlipCard());
        return _failSequence;
    }
}
