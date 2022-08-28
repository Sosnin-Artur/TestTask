using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseMover : MonoBehaviour
{    
    [SerializeField]
    protected List<Transform> Points;        

    public abstract void StartMove();    
}