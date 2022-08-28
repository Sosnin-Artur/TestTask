using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseObstacleMovableObject : BaseMovableObject
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
