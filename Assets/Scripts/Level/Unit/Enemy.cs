using System;
using UnityEngine;

public class Enemy : Unit
{
    [Serializable]
    public class Settings
    {
        public int GoldByKill = 10;
    }
    
    protected readonly EnemyMoveComponent EnemyMoveComponent;
    private readonly HealthView _healthView;
    private readonly int _goldByKill;
    
    public int GoldByKill => _goldByKill;
    public override Transform MoveTransform => EnemyMoveComponent.MoveTransform;
    
    public Enemy(AttackComponent attackComponent, 
                 HealthComponent healthComponent, 
                 EnemyMoveComponent enemyMoveComponent, 
                 HealthView healthView,
                 EnemySettings settings) : 
        base(attackComponent, healthComponent)
    {
        EnemyMoveComponent = enemyMoveComponent;
        _healthView = healthView;
        _goldByKill = settings.BaseSetting.GoldByKill;
    }

    public override void Initialize()
    {
        base.Initialize();
        
        EnemyMoveComponent.Initialize();
        _healthView.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();
        
        EnemyMoveComponent.Dispose();
        _healthView.Dispose();
    }

    public override void Tick()
    {
        base.Tick();
        
        if (_currentTarget != null && !_attackComponent.IsAllowedAttackDistance(_currentTarget))
        {
            EnemyMoveComponent.Move(_currentTarget._moveTransform.position);
        }
    }
}
