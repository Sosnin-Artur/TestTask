using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private BaseMovableObject _movableObject;
    [SerializeField]
    private LayerMask _movableObstacleLayerMask;
    [SerializeField]
    private LayerChecker _layerChecker;

    private void OnCollisionEnter(Collision other) 
    {        
        if (_layerChecker.IsInLayerMask(_movableObstacleLayerMask, other.gameObject))
        {
            Debug.Log("fuck");
            _movableObject.Reset();
        }
        // if (TryGetComponent<PoolableObstacleMovableObject>(out PoolableObstacleMovableObject movableObject))
        // {            
        //     _movableObject.Reset();
        // }        
    }
}
