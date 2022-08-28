using UnityEngine;

public abstract class BaseLayerChecker : MonoBehaviour
{
    public abstract bool IsInLayerMask(LayerMask mask, GameObject obj);    
}