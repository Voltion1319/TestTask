using Zenject;

public class PlayerInstaller : Installer<PlayerInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(Player), typeof(Unit), typeof(ITickable)).To<Player>().AsSingle();
        
        Container.Bind<UnitSettings>().FromNewScriptableObjectResource("Settings/PlayerSettings").AsSingle();
        
        Container.BindInterfacesAndSelfTo<AttackComponent>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthComponent>().AsSingle();
    }
}
