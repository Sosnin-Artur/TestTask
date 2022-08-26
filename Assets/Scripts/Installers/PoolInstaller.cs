//using System.Diagnostics;
//using System.ComponentModel;
//using System;
using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller
{
    [SerializeField]
    private BasePoolableMovableObject _movableObject;
    [SerializeField]
    private int _poolInitialSize;
    [SerializeField]
    private string _transformGroupName;

    public override void InstallBindings()
    {                
        Container
            .BindFactory<BasePoolableMovableObject, BaseMovableObject.Factory<BasePoolableMovableObject>>()                
                .FromPoolableMemoryPool<BasePoolableMovableObject, MovableObjectPool>(poolBinder => poolBinder                    
                    .WithInitialSize(_poolInitialSize)
                    .FromComponentInNewPrefab(_movableObject)
                    .UnderTransformGroup(_transformGroupName));
    }

    class MovableObjectPool : MonoPoolableMemoryPool<IMemoryPool, BasePoolableMovableObject>
    {
    }
}