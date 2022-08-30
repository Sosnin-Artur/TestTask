using System.Net.Cache;
using System;
using Zenject;

public abstract class BasePoolableObstacleMovableObject : BaseObstacleMovableObject, IPoolable<IMemoryPool>
{    
    private IMemoryPool _pool;

    public IMemoryPool Pool => _pool;

    public virtual void OnDespawned()
    {
        _pool = null;        
        Agent.isStopped = true;
        Agent.ResetPath();
        gameObject.SetActive(false);
    }

    public virtual void OnSpawned(IMemoryPool pool)
    {        
        _pool = pool;
        CurrentPointIndex = 0;        
    }    
}
