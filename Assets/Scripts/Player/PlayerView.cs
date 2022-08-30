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
    private InputAction _clickAction;
    [SerializeField]
    private InputAction _posAction;
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
        _clickAction.performed += Attack;
        _clickAction.performed += Interact;
    }

    private void OnEnable() 
    {
        TurnOnMovingControll();
    }

    public void Reset() 
    {
        _mover.MovableObject.Reset();
        _timer.Stop();        
        TurnOnMovingControll();        
    }

    private void TurnOnFightControll()
    {
        _moveAction.Disable();         

        _clickAction.Enable();
        _posAction.Enable();                
    }        

    private void TurnOnMovingControll()
    {
        _moveAction.Enable(); 

        _clickAction.Disable();
        _posAction.Disable();                
    }        

    private void Attack(InputAction.CallbackContext context) 
    {                                  
        if (_isReadyForAttack)
        {
            var position = _posAction.ReadValue<Vector2>();        

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

    private void Interact(InputAction.CallbackContext context) 
    {                                  
        var position = _posAction.ReadValue<Vector2>();        

        var ray = _camera.ScreenPointToRay(position);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {            
            if (hit.collider.TryGetComponent<Chest>(out Chest chest))
            {
                chest.Open();                                                
            }
        }                
    }     

    private IEnumerator StartCooldownAttackTimer()
    {
        Debug.Log("cooldown start");
        _isReadyForAttack = false;
        var waiter = new WaitForSecondsRealtime(_damager.CooldownAttack);
        var currentTime = 0.0f;

        while (currentTime <= _damager.CooldownAttack)
        {
            yield return waiter;            
            currentTime += Time.deltaTime;
        }

        _isReadyForAttack = true;
        Debug.Log("cooldown end");
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
        _moveAction.Disable(); 
        _moveAction.performed -= _mover.StartMoveHandler;                

        _clickAction.Disable();
        _posAction.Disable();
        _clickAction.performed -= Attack;
        _clickAction.performed -= Interact;
    }        
}
