using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTilesDatabase", menuName = "Scriptables/CardTilesDatabase")]
public class CardTilesDatabase : ScriptableObject
{
    [SerializeField]
    private List<Sprite> cards;

    public Sprite GetSprite(int id)
    {
        return cards[id];
    }

    public int GetCardsCount() 
    { 
        return cards.Count-1;
    }
}