using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class BaseMovableObject : MonoBehaviour
{    
    [SerializeField]
    private NavMeshAgent _agent;

    protected int CurrentPointIndex = 0;    

    public NavMeshAgent Agent => _agent;
    
    public List<Transform> Points { get; set; }            
    
    public void StartMove()
    {        
        _agent.destination = Points[CurrentPointIndex].position;                
    }

    public void Reset() 
    {
        Agent.isStopped = true;
        Agent.ResetPath();
        CurrentPointIndex = 1;        
        transform.position = Points[0].position;        
    }
    
    protected virtual void OnTriggerEnter(Collider other) 
    {        
        if (Points[CurrentPointIndex] == other.gameObject.transform)        
        {
            OnPointReaching();            
        }
    }

    protected abstract void OnPointReaching();

    public class Factory<T> : PlaceholderFactory<T> where T : BaseMovableObject
    {
    }
}
