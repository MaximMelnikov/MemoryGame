using DG.Tweening;
using UnityEngine;
using Zenject;

public class CardSprite : Card
{
    private CardTilesDatabase _cardTilesDatabase;
    
    [SerializeField]
    private SpriteRenderer _iconSpriteRenderer;

    private Sprite _iconSprite;

    [Inject]
    private void Construct(
        CardTilesDatabase cardTilesDatabase,
        GameController gameController,
        AudioService audioService)
    {
        _cardTilesDatabase = cardTilesDatabase;
        _gameController = gameController;
        _audioService = audioService;
    }

    public override void Init(int id)
    {
        Id = id;
        _iconSprite = _cardTilesDatabase.GetSprite(id);
        _iconSpriteRenderer.sprite = _iconSprite;
    }
}