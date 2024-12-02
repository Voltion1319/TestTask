using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum ScreenType
{
    GameScreen = 0,
    WinScreen = 1,
    LoseScreen = 2,
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private List<ScreenBase> _availableScreens;
    [SerializeField] private Transform _screenParent;

    private ScreenBase _currentScreen;
    private IInstantiator _instantiator;
    
    [Inject]
    public void Constructor(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }
    
    public void Initialize()
    {
    }

    public void Dispose()
    {
    }

    public T ShowScreen<T>(ScreenType screenType) where T : ScreenBase
    {
        if (_currentScreen != null)
        {
            _currentScreen.Hide();
            Destroy(_currentScreen.gameObject);
            _currentScreen = null;
        }

        for (int i = 0; i < _availableScreens.Count; i++)
        {
            if (_availableScreens[i].ScreenType == screenType)
            {
                _currentScreen = _instantiator.InstantiatePrefabForComponent<ScreenBase>(_availableScreens[i], _screenParent);
                _currentScreen.Show();
                break;
            }
        }

        if (_currentScreen == null)
        {
            Debug.LogError("Screen is not found");
        }

        return (T) _currentScreen;
    }
}
