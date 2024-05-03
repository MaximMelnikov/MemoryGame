using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavableData
{
    public string Name { get; }
    public (string, string) Serialize();
    public void Deserialize(string value);
}
