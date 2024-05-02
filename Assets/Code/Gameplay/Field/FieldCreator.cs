using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class FieldCreator : IFieldCreator
{
    private const float difficulty = 1f; //0-1 value, 1-all pairs are unique
    private float Difficulty { get { return difficulty; } }

    private readonly DiContainer _container;
    private readonly CardTilesDatabase _cardTilesDatabase;
    private readonly FieldSettings _fieldSettings;
    private readonly FieldSizeController _fieldSizeController;
    private readonly Transform _cardsContainer;
    private List<Card> cards;

    public List<Card> Cards { get => cards; }

    public FieldCreator(
        DiContainer container,
        CardTilesDatabase cardTilesDatabase,
        FieldSettings fieldSettings,
        FieldSizeController fieldSizeController)
    {
        _container = container;
        _cardTilesDatabase = cardTilesDatabase;
        _fieldSettings = fieldSettings;
        _fieldSizeController = fieldSizeController;
        _cardsContainer = new GameObject("CardsContainer").transform;
    }

    public void CreateField()
    {
        int cardsCount = _fieldSettings.columnsCount * _fieldSettings.rowsCount;
        if ((float)cardsCount / 2 % 1 != 0)
        {
            Debug.LogError("Please use field size that divides by 2");
        }
        int totalPairsCount = cardsCount / 2;
        int uniquePairsCount = Mathf.FloorToInt(totalPairsCount * Difficulty);
        
        if (uniquePairsCount > _cardTilesDatabase.GetCardsCount())
        {
            Debug.LogError("Card tiles count lesser then unique pairs needed");
        }
        _fieldSizeController.Resize();
        SpawnCards(cardsCount);
        ShuffleField();
    }

    public void ShuffleField()
    {
        _fieldSizeController.Resize();

        int cardsCount = _fieldSettings.columnsCount * _fieldSettings.rowsCount;
        int totalPairsCount = cardsCount / 2;
        int uniquePairsCount = Mathf.FloorToInt(totalPairsCount * Difficulty);

        if (this.cards.Count == 0)
        {
            SpawnCards(cardsCount);
        }

        int[] cardsIds = CreateShuffledCardsList(totalPairsCount, uniquePairsCount);

        for (int i = 0; i < cardsIds.Length; i++)
        {
            cards[i].Init(cardsIds[i]);
        }
    }

    private void SpawnCards(int cardsCount)
    {
        cards = new List<Card>(cardsCount);
        for (int x = 0; x < _fieldSettings.columnsCount; x++)
        {
            for (int y = 0; y < _fieldSettings.rowsCount; y++)
            {
                var card = GameObject.Instantiate(_fieldSettings.cardPrefab, _cardsContainer);
                card.transform.localPosition = CalcCardPosition(x, y);
                _container.InjectGameObject(card.gameObject);
                cards.Add(card);
            }
        }
    }

    private Vector3 CalcCardPosition(int x, int y)
    {
        float cardWorldWidth = _fieldSettings.cardWidth / (float)FieldSettings.PPU;
        float cardWorldHeight = _fieldSettings.cardHeight / (float)FieldSettings.PPU;
        float spacingWorld = _fieldSettings.spacing / (float)FieldSettings.PPU;

        float xPos = x * cardWorldWidth + x * spacingWorld;
        float yPos = y * cardWorldHeight + y * spacingWorld;

        return new Vector3(xPos, yPos, 0);
    }

    private int[] CreateShuffledCardsList(int totalPairsCount, int uniquePairsCount)
    {
        var random = new System.Random();

        //choose N random numbers from range of cards ids. Create list of numbers and random sorting them, than get first N elements
        var uniquePairsIds = Enumerable.Range(1, _cardTilesDatabase.GetCardsCount())
            .OrderBy(t => random.Next())
            .Take(uniquePairsCount)
            .ToArray();

        //add missing pairs from unique pairs array
        var missingPairsCount = totalPairsCount - uniquePairsCount;
        var missingPairsIds = uniquePairsIds
            .OrderBy(x => random.Next())
            .Take(missingPairsCount)
            .ToArray();

        int[] cards = new int[totalPairsCount*2];
        uniquePairsIds.CopyTo(cards, 0);
        uniquePairsIds.CopyTo(cards, uniquePairsIds.Length);//we need to double list because we need a pair for every card
        missingPairsIds.CopyTo(cards, uniquePairsIds.Length*2);
        missingPairsIds.CopyTo(cards, uniquePairsIds.Length * 2 + missingPairsIds.Length);

        //one more randomizer
        return cards.OrderBy(x => random.Next()).ToArray();
    }
}
