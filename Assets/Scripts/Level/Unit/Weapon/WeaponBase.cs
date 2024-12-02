using System;
using UnityEngine;

[Serializable]
public class WeaponBase
{
    [SerializeField] private DamageInfo _damageInfo;
    [SerializeField] private float _attackDistance = .5f;
    [SerializeField] private float _attackDelay = 1f;

    public DamageInfo DamageInfo => _damageInfo;

    public float AttackDistance
    {
        get => _attackDistance;
        set => _attackDistance = value;
    }

    public float AttackDelay
    {
        get => _attackDelay;
        set => _attackDelay = value;
    }
}