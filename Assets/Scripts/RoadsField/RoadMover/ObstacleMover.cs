using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using CoroutineWrapper;

public class ObstacleMover : BaseMover
{    
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _appearencePercent;
    [SerializeField]    
    private float _appearenceCooldown;
    [SerializeField]    
    private float _initialSpeed;

    private CoroutineObject _spawnCoroutine;
    private List<BasePoolableObstacleMovableObject> _currentObjects;
    private BaseMovableObject.Factory<BasePoolableObstacleMovableObject> _movableObjectsFactory;

    [Inject]
    public void Constructor(BaseMovableObject.Factory<BasePoolableObstacleMovableObject> factory)
    {        
        _movableObjectsFactory = factory;
    }

    private void Awake()
    {
        _currentObjects = new List<BasePoolableObstacleMovableObject>();    
        _spawnCoroutine = new CoroutineObject(this, StartSpawnObstacles);        
    }        

    public void Reset()
    {
        _spawnCoroutine.Stop();

        for (int i = 0, length = _currentObjects.Count; i < length; i++)
        {            
            _currentObjects[i].Pool?.Despawn(_currentObjects[i]);
        }
        StartMove();
    }

    public override void StartMove() 
    {        
        _spawnCoroutine.Start();
    }

    private IEnumerator StartSpawnObstacles()
    {
        var waiter = new WaitForSecondsRealtime(_appearenceCooldown);

        while (true)
        {
            yield return waiter;
            
            if (UnityEngine.Random.value <= _appearencePercent)
            {                                
                var movableObject =  _movableObjectsFactory.Create();
                _currentObjects.Add(movableObject);
                movableObject.ReachingEndPointEvent += () => _currentObjects.Remove(movableObject);
                movableObject.Agent.speed = _initialSpeed;
                movableObject.transform.position = Points[0].position;
                movableObject.transform.SetParent(transform);
                movableObject.gameObject.SetActive(true);
                movableObject.Points = Points;
                movableObject.StartMove();
            }
        }        

    }
    
}
