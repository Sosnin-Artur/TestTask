using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovableObject : BaseMovableObject
{            
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
