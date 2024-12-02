using UnityEngine;

public class Player : Unit
{
    private readonly Transform _mainTransform;
    public override Transform MoveTransform => _mainTransform;
    
    public Player(AttackComponent attackComponent, HealthComponent healthComponent, Transform mainTransform) : 
        base(attackComponent, healthComponent)
    {
        _mainTransform = mainTransform;
    }
}
