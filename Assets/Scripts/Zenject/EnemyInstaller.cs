using Zenject;

public class EnemyInstaller : Installer<EnemyInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(Enemy), typeof(Unit), typeof(ITickable)).To<Enemy>().AsSingle();
        
        Container.Bind(typeof(EnemySettings), typeof(UnitSettings)).To<EnemySettings>().FromNewScriptableObjectResource("Settings/EnemySettings").AsSingle();
        
        Container.BindInterfacesAndSelfTo<AttackComponent>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthComponent>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyMoveComponent>().AsSingle();
    }
}