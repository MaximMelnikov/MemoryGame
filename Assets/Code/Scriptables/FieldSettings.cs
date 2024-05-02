using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldSettings", menuName = "Scriptables/FieldSettings")]
public class FieldSettings : ScriptableObject
{
    public const int PPU = 100; //tiles pixel per unit
    public const int backCardId = 0;

    [Header("Field settings")]
    public int rowsCount = 4;
    public int columnsCount = 4;

    [Header("Cards settings")]
    public int cardWidth = 54;
    public int cardHeight = 90;
    public int spacing = 10;

    public Card cardPrefab;

    [Header("Game settings")]
    public int showCardsTime = 5; //stage on game start when all cards are open
    public int timeToFail = 60;
}