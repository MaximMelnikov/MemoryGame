using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FieldSettings", menuName = "Scriptables/FieldSettings")]
public class FieldSettings : ScriptableObject
{
    public const int backCardId = 0;

    [Header("Field settings")]
    public bool fitScreen = true; //discards defaultFieldSize if true
    public int rowsCount = 4;
    public int columnsCount = 4;

    [Header("Cards settings")]
    public int cardWidth = 54;
    public int cardHeight = 90;
    public int space = 10;

    public Card cardPrefab;
}