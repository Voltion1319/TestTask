using System;
using UnityEngine;
using Zenject;

public class UnitFacade : MonoBehaviour, IDamagable
{
    public event Action<UnitFacade> OnDead;
    
    private Unit _model;

    [Inject]
    public void Construct(Unit model)
    {
        _model = model;
    }

    public bool IsAlive => _model.IsAlive;
    public Transform _moveTransform => _model.MoveTransform;

    public virtual void Initialize()
    {
        _model.Initialize();
        _model.OnDead += ModelOnOnDead;
    }

    public virtual void Dispose()
    {
        _model.OnDead -= ModelOnOnDead;
        _model.Dispose();
    }

    public void GetDamage(DamageInfo damageInfo)
    {
        _model.GetDamage(damageInfo);
    }
    
    private void ModelOnOnDead(Unit unit)
    {
        OnDead?.Invoke(this);
    }


}