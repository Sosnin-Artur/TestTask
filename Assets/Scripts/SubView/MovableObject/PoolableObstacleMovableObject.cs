using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class PoolableObstacleMovableObject : BasePoolableObstacleMovableObject
{    
    protected override void OnPointReaching()
    {
        CurrentPointIndex++;
        if (CurrentPointIndex == Points.Count)
        {
            Pool.Despawn(this);
            return;
        }
        
        Agent.destination = Points[CurrentPointIndex].position;
    }    
}
