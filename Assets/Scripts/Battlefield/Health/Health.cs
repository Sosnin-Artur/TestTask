using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Health 
{
    public event Action DeathEvent;

    [SerializeField]
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            if (value <= 0)
            {                
                DeathEvent?.Invoke();
                return;
            }
            _value = value;
        } 
    } 
}