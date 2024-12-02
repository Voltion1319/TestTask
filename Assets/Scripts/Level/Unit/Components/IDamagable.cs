using UnityEngine;

public interface IDamagable
{
    public bool IsAlive { get; }
    public Transform _moveTransform { get; }
    
    public void GetDamage(DamageInfo damageInfo);
}