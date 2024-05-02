using Core.Services.Input;
using DG.Tweening;
using Lean.Touch;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Card : MonoBehaviour, IInputInteractable
{
    private CardTilesDatabase _cardTilesDatabase;
    [SerializeField]
    private float _flipTime = .2f;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public int Id { get; private set; }
    public bool IsFaced { get; private set; }
    public bool IsInputEnabled { get; set; }

    [Inject]
    private void Construct(CardTilesDatabase cardTilesDatabase)
    {
        _cardTilesDatabase = cardTilesDatabase;
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

    public async Task FlipCard()
    {
        IsInputEnabled = false;
        await _spriteRenderer.transform.DORotate(new Vector3(0, 90, 0), _flipTime).AsyncWaitForCompletion();
        SetSprite(IsFaced ? FieldSettings.backCardId : Id);
        await _spriteRenderer.transform.DORotate(new Vector3(0, 180, 0), _flipTime).AsyncWaitForCompletion();
        IsFaced = !IsFaced;
        IsInputEnabled = true;
    }

    public void InputAction(LeanFinger finger)
    {
        Debug.Log("TowerSpot tapped");
        FlipCard();
    }
}
