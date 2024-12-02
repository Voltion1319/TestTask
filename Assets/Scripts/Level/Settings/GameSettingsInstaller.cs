using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SceneSettings", menuName = "ScriptableObjects/SceneSettings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public SceneInstaller.Settings SceneSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(SceneSettings);
    }
}
