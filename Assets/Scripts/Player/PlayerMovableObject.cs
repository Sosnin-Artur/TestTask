using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovableObject : BaseMovableObject
{            
    public override void Reset()
    {
        base.Reset();
        Agent.destination = Points[0].position;
    }

    protected override void OnPointReaching()
    {        
        CurrentPointIndex++;        

        if (CurrentPointIndex >= Points.Count)
        {            
            CurrentPointIndex = Points.Count - 1;
            OnReachingEndPoint();
            return;
        }                
    }
}
