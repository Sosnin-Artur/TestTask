using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediatePoint : BasePoint
{
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        base.OnDrawGizmos();
    }    
#endif
}
