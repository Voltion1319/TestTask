using UnityEngine;

public class LevelManager
{
    private readonly Level.Factory _levelFactory;
    private readonly ScreenManager _screenManager;
    private Level _currentLevel;
    
    public Level CurrentLevel => _currentLevel;

    public LevelManager(Level.Factory levelFactory, 
                        ScreenManager screenManager)
    {
        _levelFactory = levelFactory;
        _screenManager = screenManager;
    }

    public void Initialize()
    {
        SpawnLevel();
    }

    public void Dispose()
    {
        DisposeLevel();
    }
    
    public void NextLevel()
    {
        if (_currentLevel.IsWin)
        {
            SpawnLevel();
        }
        else
        {
            SpawnLevel();
        }
    }

    private void SpawnLevel()
    {
        DisposeLevel();

        _currentLevel = _levelFactory.Create();
        _currentLevel.Initialize();
        _currentLevel.OnLevelEnd += OnOnLevelEnd;
    }

    private void DisposeLevel()
    {
        if (_currentLevel != null)
        {
            _currentLevel.Dispose();
            Object.Destroy(_currentLevel.gameObject);
            _currentLevel = null;
        }
    }

    private void OnOnLevelEnd(bool result)
    {
        if (result)
        {
            _screenManager.ShowScreen<WinScreen>(ScreenType.WinScreen);
        }
        else
        {
            _screenManager.ShowScreen<LoseScreen>(ScreenType.LoseScreen);
        }
    }
}
