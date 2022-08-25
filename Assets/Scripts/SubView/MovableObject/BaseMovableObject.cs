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
    
    public bool IsReady { get; private set; }
    
    public void StartMove()
    {
        IsReady = true;
        _agent.destination = Points[CurrentPointIndex].position;                
    }

    protected virtual void OnTriggerEnter(Collider other) 
    {        
        if (IsReady && Points[CurrentPointIndex] == other.gameObject.transform)        
        {
            OnPointReaching();            
        }
    }

    protected abstract void OnPointReaching();

    public class Factory<T> : PlaceholderFactory<T> where T : BaseMovableObject
    {
    }
}
