using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/EnemySettings")]
public class EnemySettings: UnitSettings
{
    public EnemyMoveComponent.Settings MoveSettings;
    public Enemy.Settings BaseSetting;
}