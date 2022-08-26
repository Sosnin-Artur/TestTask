using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableObject : BaseMovableObject
{    
    protected override void OnPointReaching()
    {
        CurrentPointIndex++;
        if (CurrentPointIndex == Points.Count)
        {
            Destroy(gameObject);    
            return;
        }
        
        Agent.destination = Points[CurrentPointIndex].position;
    }
}
