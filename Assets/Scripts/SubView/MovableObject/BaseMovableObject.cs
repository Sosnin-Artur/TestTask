using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class BaseMovableObject : MonoBehaviour
{    
    public event Action ReachingEndPointEvent;

    [SerializeField]
    private NavMeshAgent _agent;

    protected int CurrentPointIndex = 0;        

    public NavMeshAgent Agent => _agent;
    
    public List<Transform> Points { get; set; }            
    
    public virtual void StartMove()
    {        
        _agent.destination = Points[CurrentPointIndex].position;                
    }
    
    public virtual void StopMove()
    {    
        if (gameObject.activeInHierarchy)
        {
            if (!Agent.isStopped)
            {
                Agent.isStopped = true;
                Agent.ResetPath();
            }        
        }                    
    }

    public virtual void Reset() 
    {
        StopMove();
        CurrentPointIndex = 0;        
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

    public virtual void OnReachingEndPoint()
    {
        ReachingEndPointEvent?.Invoke();
    }

    public class Factory<T> : PlaceholderFactory<T> where T : BaseMovableObject
    {
    }
}
