using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFieldCreator
{
    public List<Card> Cards { get; }
    public void CreateField();
    public void ShuffleField();
}
