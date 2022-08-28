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

    private PlayerInputActions _controls;

    private void Awake()
    {
        _movableObject.Points = Points;      
        _movableObject.transform.position = Points[0].position;

        _controls = new PlayerInputActions();
        _controls.Player.Walking.performed += context => StartMove();
    }
    
    private void OnEnable() 
    {
        _controls.Enable();        
    }
    
    private void OnDisable() 
    {
        _controls.Disable();        
    }    

    public override void StartMove() 
    {                  
        _movableObject.StartMove();
    }        
}
