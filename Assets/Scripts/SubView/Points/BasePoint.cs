using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePoint : MonoBehaviour
{    
#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {                        
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);    
    }
#endif
}
