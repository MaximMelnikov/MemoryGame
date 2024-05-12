using System;

[Serializable]
public class BoolOptionEntity : IOptionEntity<bool>, ISavableData
{
    public event Action<bool> OnOptionChanged;
    public string Name { get; set; }
    private bool _value;
    public bool Value
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

    public BoolOptionEntity(string name)
    {
        Name = name;
    }

    public (string, string) Serialize()
    {
        return (Name, Value.ToString());
    }

    public void Deserialize(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }
        Value = Convert.ToBoolean(value);
    }
}