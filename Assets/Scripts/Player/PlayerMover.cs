using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CoroutineWrapper;
using UnityEngine.InputSystem;

public class PlayerMover : BaseMover
{        
    [SerializeField]
    private BaseMovableObject _movableObject;    

    public BaseMovableObject MovableObject => _movableObject;

    private void Awake()
    {
        _movableObject.Points = Points;      
        _movableObject.transform.position = Points[0].position;                
    }    

    public void StartMoveHandler(InputAction.CallbackContext context)
    {
        StartMove();
    }

    public override void StartMove() 
    {                          
        Debug.Log("check");
        _movableObject.StartMove();
    }               
}
