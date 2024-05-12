using System;

public interface IOptionEntity<T>
{
    public string Name { get; set; }
    public T Value { get; set; }

    public event Action<T> OnOptionChanged;
}