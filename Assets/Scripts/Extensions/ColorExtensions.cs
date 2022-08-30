using UnityEngine;

public static class ColorExtensions
{
    public static bool EqualsTo(this Color color, Color other)
    {                
        return Mathf.Approximately(color.r, other.r)
            & Mathf.Approximately(color.g, other.g)
            & Mathf.Approximately(color.b, other.b);            
    }    
}