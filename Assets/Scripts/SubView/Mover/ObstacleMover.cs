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

    private CoroutineObject _spawnCoroutine;

    private BaseMovableObject.Factory<BasePoolableObstacleMovableObject> _movableObjectsFactory;

    [Inject]
    public void Constructor(BaseMovableObject.Factory<BasePoolableObstacleMovableObject> factory)
    {        
        _movableObjectsFactory = factory;
    }

    private void Awake()
    {
        _spawnCoroutine = new CoroutineObject(this, StartSpawnObstacles);
    }

    private void Start()
    {                
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
                var movablObject =  _movableObjectsFactory.Create();
                movablObject.transform.position = Points[0].position;
                movablObject.transform.SetParent(transform);
                movablObject.gameObject.SetActive(true);
                movablObject.Points = Points;
                movablObject.StartMove();
            }
        }        

    }
    
}
