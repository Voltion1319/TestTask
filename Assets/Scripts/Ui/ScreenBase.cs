using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBase : MonoBehaviour
{
    [SerializeField] private ScreenType _screenType;

    public ScreenType ScreenType => _screenType;
    
    public virtual void Show()
    {
        
    }

    public virtual void Hide()
    {
        
    }
}
