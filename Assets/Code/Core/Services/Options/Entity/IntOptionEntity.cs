using System;
using UnityEngine;

public class IntOptionEntity : IOptionEntity<int>, ISavableData
{
    public event Action<int> OnOptionChanged;
    public string Name { get; set; }
    
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (_value != value) 
            {
                OnOptionChanged?.Invoke(value);
            }
            _value = value;
        }
    }

    public IntOptionEntity(string name)
    {
        Name = name;
    }

    public (string, string) Serialize()
    {
        return (Name, Value.ToString());
    }

    public void Deserialize(string value)
    {
        Value = Convert.ToInt32(value);
    }
}
