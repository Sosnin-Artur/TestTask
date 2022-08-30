using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField]
    private Transform _target;    
    [SerializeField]
    private List<EnemyView> _enemies;
    [SerializeField]
    private PlayerView _player;
    [SerializeField]
    private Chest _chest;

    private int _deadEnemiesCount = 0;    
    
    private void Awake()
    {
        _player.Mover.MovableObject.ReachingEndPointEvent += StartAttack;
        _player.DeathEvent += Reset;

        for (int i = 0, length = _enemies.Count; i < length; i++)
        {            
            _enemies[i].DeathEvent += OnEnemyDeath;
        }
    }

    public void Reset()
    {
        _chest.gameObject.SetActive(false);

        _deadEnemiesCount = 0;                

        ResetEnemies();
    }

    public void ResetEnemies()
    {        
        for (int i = 0, length = _enemies.Count; i < length; i++)
        {
            _enemies[i].Reset();
        }
    }

    public void OnEnemyDeath()
    {        
        _deadEnemiesCount++;

        if (_deadEnemiesCount == _enemies.Count)
        {                     
            _chest.gameObject.SetActive(true);
        }
    }

    public void StartAttack()
    {                
        for (int i = 0, length = _enemies.Count; i < length; i++)
        {
            _enemies[i].SendToTarget(_target);
        }
    }

}
