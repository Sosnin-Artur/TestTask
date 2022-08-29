using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CoroutineWrapper;

public class PlayerView : MonoBehaviour
{
    public event Action DeathEvent;
        
    [SerializeField]
    private PlayerMover _mover;
    [SerializeField]
    private Damager _damager;    
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private InputAction _moveAction;
    [SerializeField]
    private InputAction _attackAction;
    [SerializeField]
    private InputAction _attackPosAction;
    [SerializeField]
    private LayerMask _movableObstacleLayerMask;
    [SerializeField]
    private LayerChecker _layerChecker;

    private CoroutineObject _timer;   
    private bool _isReadyForAttack = true;
    
    public PlayerMover Mover => _mover;

    private void Awake()
    {        
        _timer = new CoroutineObject(this, StartCooldownAttackTimer);
        _mover.MovableObject.ReachingEndPointEvent += TurnOnFightControll;

        _moveAction.performed += _mover.StartMoveHandler;                        
        _attackAction.performed += Attack;
    }

    private void OnEnable() 
    {
        TurnOnMovingControll();
    }

    private void TurnOnFightControll()
    {
        _moveAction.Disable();         

        _attackAction.Enable();
        _attackPosAction.Enable();                
    }        

    private void TurnOnMovingControll()
    {
        _moveAction.Enable(); 

        _attackAction.Disable();
        _attackPosAction.Disable();                
    }        

    private void Attack(InputAction.CallbackContext context) 
    {                          
        if (_isReadyForAttack)
        {
            var position = _attackPosAction.ReadValue<Vector2>();        

            var ray = _camera.ScreenPointToRay(position);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {            
                if (hit.collider.TryGetComponent<EnemyView>(out EnemyView enemy))
                {
                    enemy.TakeDamage(_damager.DamageValue);
                    enemy.Rigidbody.AddForce(
                        (enemy.transform.position - transform.position).normalized * _damager.KnockbackForce);
                    _timer.Start();
                }
            }
        }        
    }     

    private IEnumerator StartCooldownAttackTimer()
    {
        Debug.Log("culldown start");
        _isReadyForAttack = false;
        var waiter = new WaitForSecondsRealtime(_damager.CooldownAttack);
        var currentTime = 0.0f;

        while (currentTime <= _damager.CooldownAttack)
        {
            yield return waiter;            
            currentTime += Time.deltaTime;
        }

        _isReadyForAttack = true;
        Debug.Log("culldown end");
    }

    private void OnCollisionEnter(Collision other) 
    {        
        if (_layerChecker.IsInLayerMask(_movableObstacleLayerMask, other.gameObject))
        {            
            _mover.MovableObject.Reset();
            TurnOnMovingControll();
            DeathEvent?.Invoke();
        }        
    }     
    
    private void OnDisable() 
    {
        _moveAction.Enable(); 
        _moveAction.performed -= _mover.StartMoveHandler;                

        _attackAction.Disable();
        _attackPosAction.Disable();
        _attackAction.performed -= Attack;
    }        
}
