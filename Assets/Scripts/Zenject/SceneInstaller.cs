using System;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [Serializable]
    public class Settings
    {
        public Level LevelPrefab;
    }
    
    [Inject]
    Settings _settings = null;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<StartProject>().AsSingle();
        
        Container.Bind<LevelManager>().AsSingle();
        Container.Bind<Wallet>().AsSingle();

        Container.BindFactory<Level, Level.Factory>().FromComponentInNewPrefab(_settings.LevelPrefab);
    }
}