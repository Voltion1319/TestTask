using System;
using UnityEngine;
using Zenject;

public abstract class Unit : ITickable
{
    public event Action<Unit> OnDead;
    
    protected readonly AttackComponent _attackComponent;
    protected readonly HealthComponent _healthComponent;
    protected IDamagable _currentTarget;

    public bool IsAlive => _healthComponent.IsAlive;
    public abstract Transform MoveTransform { get; }

    public Unit(AttackComponent attackComponent, HealthComponent healthComponent)
    {
        _attackComponent = attackComponent;
        _healthComponent = healthComponent;
    }

    public virtual void Initialize()
    {
        _healthComponent.Initialize();
        _attackComponent.Initialize();

        _healthComponent.OnHealthEmpty += HealthComponentOnOnHealthEmpty;
    }

    public virtual void Dispose()
    {
        _healthComponent.OnHealthEmpty -= HealthComponentOnOnHealthEmpty;
        
        _attackComponent.Dispose();
        _healthComponent.Dispose();
    }
    
    public virtual void Tick()
    {
        if (_currentTarget == null)
        {
            _currentTarget = _attackComponent.FindNearestTarget();
            if (_currentTarget == null)
            {
                return;
            }
        }

        if (!_currentTarget.IsAlive)
        {
            _currentTarget = null;
            return;
        }

        if (_attackComponent.IsAllowedAttackDistance(_currentTarget))
        {
            _attackComponent.TryDamage(_currentTarget);
        }
    }

    public void GetDamage(DamageInfo damageInfo)
    {
        _healthComponent.GetDamage(damageInfo);
    }

    protected virtual void Dead()
    {
        OnDead?.Invoke(this);
    }
    
    private void HealthComponentOnOnHealthEmpty()
    {
        Dead();
    }
}
