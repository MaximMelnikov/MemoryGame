using System;
using System.Collections.Generic;

public interface IFieldCreator : IDisposable
{
    public List<Card> Cards { get; }
    public void CreateField();
    public void ShuffleField();
}
