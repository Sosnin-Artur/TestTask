using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Damager 
{
    [SerializeField]
    private float _cooldownAttack;
    [SerializeField]
    private float _knockbackForce;        
    [SerializeField]
    private int _damageValue;

    public float CooldownAttack => _cooldownAttack;
    public float KnockbackForce => _knockbackForce;
    public int DamageValue => _damageValue;    
}
