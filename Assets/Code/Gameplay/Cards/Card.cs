using Core.Services.Input;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, IInputInteractable
{
    public bool IsFaced { get; private set; }
    public int Id { get; private set; }
    public bool IsInputEnabled { get; set; }

    private void Awake()
    {
        IsInputEnabled = true;
    }

    public void Init(int id)
    {
        
    }

    private void LoadGraphics()
    {
        
    }

    public void FlipCard()
    {

    }

    public void InputAction(LeanFinger finger)
    {
        Debug.Log("TowerSpot tapped");
    }
}
