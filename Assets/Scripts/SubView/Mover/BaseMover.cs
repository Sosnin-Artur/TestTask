﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseMover : MonoBehaviour
{
    [SerializeField]
    protected BaseMovableObject CurrentObject;
    [SerializeField]
    protected List<Transform> Points;    
    
    private void Start()
    {                
        StartMove();
    }     

    public abstract void StartMove();    
}