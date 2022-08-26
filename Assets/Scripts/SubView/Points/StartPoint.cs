using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : BasePoint
{
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        base.OnDrawGizmos();
    }    
#endif
}
