using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : BasePoint
{
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        base.OnDrawGizmos();
    }   
#endif 
}
