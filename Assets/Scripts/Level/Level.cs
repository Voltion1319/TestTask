using System;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    [Serializable]
    public class Settings
    {
        public int EnemiesCountToWin = 10;
    }
    
    public event Action OnLevelStart;
    public event Action<bool> OnLevelEnd;

    private PlayerSpawner _playerSpawner;
    private EnemySpawner _enemySpawner;
    private Wallet _wallet;
    private Settings _settings;
    private ScreenManager _screenManager;
    private int _currentEnemiesDeadCount;
    private bool _isWin = false;

    public bool IsWin => _isWin;

    [Inject]
    public void Constructor(PlayerSpawner playerSpawner, 
                            EnemySpawner enemySpawner, 
                            Wallet wallet, 
                            Settings settings, 
                            ScreenManager screenManager)
    {
        _playerSpawner = playerSpawner;
        _enemySpawner = enemySpawner;
        _wallet = wallet;
        _settings = settings;
        _screenManager = screenManager;
    }
    
    public void Initialize()
    {
        _currentEnemiesDeadCount = _settings.EnemiesCountToWin;
        
        _playerSpawner.Initialize();
        _enemySpawner.Initialize();
        _wallet.Initialize();
        
        _playerSpawner.OnPlayerDead += PlayerSpawnerOnOnPlayerDead;
        _enemySpawner.OnEnemyDead += EnemySpawnerOnOnEnemyDead;

        _screenManager.ShowScreen<GameScreen>(ScreenType.GameScreen);
        
        OnLevelStart?.Invoke();
    }

    public void Dispose()
    {
        StopSpawners();
        
        _wallet.Dispose();
    }
    
    private void Win()
    {
        StopSpawners();
        _isWin = true;
        
        OnLevelEnd?.Invoke(true);
    }
    
    private void Lose()
    {
        StopSpawners();
        
        OnLevelEnd?.Invoke(false);
    }

    private void StopSpawners()
    {
        _playerSpawner.OnPlayerDead -= PlayerSpawnerOnOnPlayerDead;
        _enemySpawner.OnEnemyDead -= EnemySpawnerOnOnEnemyDead;
        
        _playerSpawner.Dispose();
        _enemySpawner.Dispose();
    }
    
    private void PlayerSpawnerOnOnPlayerDead()
    {
        Lose();
    }
    
    private void EnemySpawnerOnOnEnemyDead()
    {
        _currentEnemiesDeadCount--;

        if (_currentEnemiesDeadCount == 0)
        {
            Win();
        }
    }
    
    public class Factory : PlaceholderFactory<Level>
    {
    }
}
