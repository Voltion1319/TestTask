using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings")]
public class LevelSettingsInstaller : ScriptableObjectInstaller<LevelSettingsInstaller>
{
    public EnemySpawner.Settings EnemySpawner;
    public Level.Settings LevelSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(EnemySpawner).IfNotBound();
        Container.BindInstance(LevelSettings).IfNotBound();
    }
}