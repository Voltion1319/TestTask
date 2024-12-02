using Zenject;

public class PlayerFacade : UnitFacade, IPoolable<IMemoryPool>
{
    private Player _playerModel;
    private IMemoryPool _pool;

    public Player PlayerModel => _playerModel;
    
    [Inject]
    public void Construct(Player model)
    {
        _playerModel = model;
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

    public class Factory : PlaceholderFactory<PlayerFacade>
    {
    }

}