using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class UnitSettings : ScriptableObject
{
    public AttackComponent.Settings AttackSet;
    public HealthComponent.Settings HealthSettings;
}
