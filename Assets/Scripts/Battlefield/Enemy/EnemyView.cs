using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public event Action DeathEvent;
        
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Health _health;
    [SerializeField]
    private BaseMovableObject _movableObject;
    [SerializeField]
    private Transform _startPoint;     

    public Health Health => _health;
    public Rigidbody Rigidbody => _rb;

    private void Awake()
    {        
        _health.DeathEvent += Die;
    }

    public void Reset()
    {
        transform.position = _startPoint.position;     
        transform.rotation = Quaternion.identity;
        _movableObject.StopMove();   
    }

    public void SendToTarget(Transform target)
    {
        _movableObject.Points = new List<Transform>();        
        _movableObject.Points.Add(target);
        _movableObject.StartMove();
    }

    public void TakeDamage(int value)
    {        
        _health.Value -= value;              
    }    

    private void Die()
    {        
        DeathEvent?.Invoke();
        gameObject.SetActive(false);
        Reset();
    }    
}
