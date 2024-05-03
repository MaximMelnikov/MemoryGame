using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOptionEntity<T>
{
    public string Name { get; set; }
    public T Value { get; set; }

    public event Action<T> OnOptionChanged;
}