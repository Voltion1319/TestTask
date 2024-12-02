using System;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [Serializable]
    public class Settings
    {
        public EnemySpawnInfo EnemySpawnInfo;
        public Transform PlayerSpawnPoint;
        public Transform UnitsParent;
        public EnemyFacade EnemyPrefab;
        public PlayerFacade PlayerPrefab;
    }
    
    [SerializeField] Settings _settings;
    
    public override void InstallBindings()
    {
        Container.Bind<EnemySpawner>().AsSingle().WithArguments(_settings.UnitsParent, _settings.EnemySpawnInfo);
        
        Container.BindFactory<EnemyFacade, EnemyFacade.Factory>()
            .FromPoolableMemoryPool<EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<EnemyInstaller>(_settings.EnemyPrefab));
        
        Container.Bind<PlayerSpawner>().AsSingle().WithArguments(_settings.UnitsParent, _settings.PlayerSpawnPoint);
        
        Container.BindFactory<PlayerFacade, PlayerFacade.Factory>()
            .FromPoolableMemoryPool<PlayerFacade, PlayerFacadePool>(poolBinder => poolBinder
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_settings.PlayerPrefab));
    }
    
    class PlayerFacadePool : MonoPoolableMemoryPool<IMemoryPool, PlayerFacade>
    {
    }
    
    class EnemyFacadePool : MonoPoolableMemoryPool<IMemoryPool, EnemyFacade>
    {
    }
}
