using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsField : MonoBehaviour
{
    [SerializeField]
    private List<ObstacleMover> _obstacleMovers;
    [SerializeField]    
    private PlayerView _player;

    private void Awake()
    {
        _player.DeathEvent += Reset;
    }

    private void Start()
    {                
        for (int i = 0, length = _obstacleMovers.Count; i < length; i++)
        {            
            _obstacleMovers[i].StartMove();
        }
    } 

    private void Reset()
    {
        for (int i = 0, length = _obstacleMovers.Count; i < length; i++)
        {            
            _obstacleMovers[i].Reset();
        }
    }
}
