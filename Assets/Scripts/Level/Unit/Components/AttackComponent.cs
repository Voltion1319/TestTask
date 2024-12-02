using System;
using UnityEngine;
using Zenject;

public class AttackComponent : ITickable 
{
    [Serializable]
    public class Settings
    {
        public WeaponBase Weapon;
        public float ViewRadius = 10f;
        public LayerMask AttackLayerMask;
    }
    
    private readonly Transform _mainTransform;
    private readonly Settings _settings;
    private float _weaponAttackDelay;
    private float _currentWeaponCooldown = 0;

    public AttackComponent(Transform mainTransform, UnitSettings settings)
    {
        _mainTransform = mainTransform;
        _settings = settings.AttackSet;
    }
    
    public virtual void Initialize()
    {
        _weaponAttackDelay = _settings.Weapon.AttackDelay;
    }

    public virtual void Dispose()
    {
        
    }
    
    public void Tick()
    {
        if (_weaponAttackDelay > 0)
        {
            _currentWeaponCooldown -= Time.deltaTime;
        }
    }

    public bool IsAllowedAttackDistance(IDamagable target)
    {
        return Vector3.Distance(target._moveTransform.transform.position, _mainTransform.position) < _settings.Weapon.AttackDistance;
    }

    public bool CanAttack(IDamagable target)
    {
        return target.IsAlive && _currentWeaponCooldown <= 0 && IsAllowedAttackDistance(target);
    }

    public IDamagable FindNearestTarget()
    {
        Collider[] targets = Physics.OverlapSphere(_mainTransform.position, _settings.ViewRadius, _settings.AttackLayerMask);

        if (targets.Length > 0)
        {
            IDamagable nearestTargetCollider = null;
            float nearestDistance = float.MaxValue;
            for (int i = 0; i < targets.Length; i++)
            {
                IDamagable possiblyTarget;
                if (targets[i].TryGetComponent(out possiblyTarget) && possiblyTarget.IsAlive)
                {
                    float currentDistance = Vector3.Distance(targets[i].transform.position, _mainTransform.position);
                    if (nearestDistance > currentDistance)
                    {
                        nearestTargetCollider = possiblyTarget;
                        nearestDistance = currentDistance;
                    }
                }
            }
            
            return nearestTargetCollider;
        }

        return null;
    }

    public bool TryDamage(IDamagable target)
    {
        if (CanAttack(target))
        {
            target.GetDamage(_settings.Weapon.DamageInfo);
            _currentWeaponCooldown = _weaponAttackDelay;
            return true;
        }

        return false;
    }
}
