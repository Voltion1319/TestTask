using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner
{
    [Serializable]
    public class Settings
    {
        public float SpawnEnemiesOffsetOverTheScreen = 1f;
        public LayerMask GroundLayer;
        public Vector2 MinMaxRandomSpawnTime = new Vector2(3,5);
    }
    
    public event Action OnEnemyDead;

    private readonly Transform _unitsParent;
    private readonly LevelManager _levelManager;
    private readonly EnemyFacade.Factory _enemyFactory;
    private readonly EnemySpawnInfo _spawnInfo;
    private readonly Settings _settings;
    private readonly Camera _mainCamera;
    private readonly Wallet _wallet;
    private readonly List<EnemyFacade> _enemies = new List<EnemyFacade>();
    private bool _isActive;
    
    public EnemySpawner(Transform unitsParent, 
                        LevelManager levelManager, 
                        EnemyFacade.Factory enemyFactory, 
                        EnemySpawnInfo spawnInfo, 
                        Settings settings,
                        Camera mainCamera,
                        Wallet wallet)
    {
        _unitsParent = unitsParent;
        _levelManager = levelManager;
        _enemyFactory = enemyFactory;
        _spawnInfo = spawnInfo;
        _settings = settings;
        _mainCamera = mainCamera;
        _wallet = wallet;
    }
    
    public void Initialize()
    {
        _isActive = true;

        RaycastHit hit;
        Ray sideOfScreenRay = _mainCamera.ViewportPointToRay(new Vector3(1f, 0.5f));
        if (Physics.Raycast(sideOfScreenRay, out hit, Mathf.Infinity, _settings.GroundLayer))
        {
            Vector3 parentSpawnPosition = _spawnInfo.SpawnPointsParent.position;
            parentSpawnPosition.x = hit.point.x + _settings.SpawnEnemiesOffsetOverTheScreen;
            _spawnInfo.SpawnPointsParent.position = parentSpawnPosition;
        }
        
        _levelManager.CurrentLevel.OnLevelStart += CurrentLevelOnOnLevelStart;
    }
    
    public void Dispose()
    {
        _isActive = false;
        
        _levelManager.CurrentLevel.OnLevelStart -= CurrentLevelOnOnLevelStart;
    
        for (int i = 0; i < _enemies.Count; i++)
        {
            DisposeEnemy(_enemies[i]);
        }
        _enemies.Clear();
    }
    
    public void StartSpawning()
    {
        StartSpawnEnemies();
    }
    
    private void StartSpawnEnemies()
    {
        SpawnProcess().Forget();
    }
    
    private void DisposeEnemy(EnemyFacade enemy)
    {
        enemy.OnDead -= EnemyOnOnDead;
        enemy.Dispose();
        _enemies.Remove(enemy);
    }
    
    private void EnemyOnOnDead(UnitFacade unit)
    {
        EnemyFacade enemyFacade = unit as EnemyFacade;
        _wallet.AddGold(enemyFacade.EnemyModel.GoldByKill);
        DisposeEnemy(enemyFacade);
        OnEnemyDead?.Invoke();
    }
    
    private async UniTaskVoid SpawnProcess()
    {
        while (_isActive)
        {
            EnemyFacade enemy =  _enemyFactory.Create();
            enemy.transform.parent = _unitsParent;
            enemy.transform.position = _spawnInfo.EnemiesSpawnPoints[Random.Range(0, _spawnInfo.EnemiesSpawnPoints.Count)].position;
            enemy.Initialize();
            enemy.OnDead += EnemyOnOnDead;
            _enemies.Add(enemy);
    
            await UniTask.WaitForSeconds(Random.Range(_settings.MinMaxRandomSpawnTime.x, _settings.MinMaxRandomSpawnTime.y));
        }
    }
    
    private void CurrentLevelOnOnLevelStart()
    {
        StartSpawning();
    }
}
