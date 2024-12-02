using Zenject;

public class EnemyFacade : UnitFacade, IPoolable<IMemoryPool>
{
    private Enemy _enemyModel;
    private IMemoryPool _pool;

    public Enemy EnemyModel => _enemyModel;
    
    [Inject]
    public void Construct(Enemy model)
    {
        _enemyModel = model;
    }
    
    public override void Dispose()
    {
        base.Dispose();
        
        _pool.Despawn(this);
    }
    
    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
    }
    
    public class Factory : PlaceholderFactory<EnemyFacade>
    {
    }
}