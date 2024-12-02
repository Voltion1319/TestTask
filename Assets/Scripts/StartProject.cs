using System;
using Zenject;

public class StartProject : IInitializable, IDisposable
{
    private readonly LevelManager _levelManager;
    private readonly ScreenManager _screenManager;
    private readonly Wallet _wallet;

    public StartProject(LevelManager levelManager, ScreenManager screenManager, Wallet wallet)
    {
        _levelManager = levelManager;
        _screenManager = screenManager;
        _wallet = wallet;
    }

    public void Initialize()
    {
        _screenManager.Initialize();
        _levelManager.Initialize();
        _wallet.Initialize();
    }

    public void Dispose()
    {
        _levelManager.Dispose();
        _screenManager.Dispose();
        _wallet.Dispose();
    }
}
