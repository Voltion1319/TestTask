using System;
using UnityEngine;

public class PlayerSpawner
{
    public event Action OnPlayerDead;
    
    private readonly Transform _unitsParent;
    private readonly LevelManager _levelManager;
    private readonly Transform _spawnParent;
    private PlayerFacade _currentPlayer;
    private readonly PlayerFacade.Factory _playerFactory;

    public PlayerSpawner(Transform unitsParent, 
                         LevelManager levelManager,
                         PlayerFacade.Factory playerFactory,
                         Transform spawnParent)
    {
        _unitsParent = unitsParent;
        _levelManager = levelManager;
        _spawnParent = spawnParent;
        _playerFactory = playerFactory;
    }
    
    public void Initialize()
    {
        _levelManager.CurrentLevel.OnLevelStart += CurrentLevelOnOnLevelStart;
    }

    public void Dispose()
    {
        DisposePlayer();
        
        _levelManager.CurrentLevel.OnLevelStart -= CurrentLevelOnOnLevelStart;
    }

    public void StartSpawning()
    {
        SpawnNewPlayer();
    }

    private void SpawnNewPlayer()
    {
        _currentPlayer = _playerFactory.Create();
        Transform transform = _currentPlayer.transform;
        transform.parent = _unitsParent;
        transform.position = _spawnParent.position;
        _currentPlayer.Initialize();
        _currentPlayer.PlayerModel.OnDead += PlayerOnOnDead;
    }

    private void DisposePlayer()
    {
        if (_currentPlayer != null)
        {
            _currentPlayer.PlayerModel.OnDead -= PlayerOnOnDead;
            _currentPlayer.Dispose();
            _currentPlayer = null;
        }
    }
    
    private void PlayerOnOnDead(Unit unit)
    {
        OnPlayerDead?.Invoke();
    }
    
    private void CurrentLevelOnOnLevelStart()
    {
        StartSpawning();
    }
}
