﻿using System.Collections.Generic;

public interface IFieldCreator
{
    public List<Card> Cards { get; }
    public void CreateField();
    public void ShuffleField();
}
