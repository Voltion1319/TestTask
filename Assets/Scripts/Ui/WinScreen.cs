using UnityEngine;

public class WinScreen : ScreenBase
{
    [SerializeField] private NextLevelButton _nextLevelButton;
    
    public override void Show()
    {
        base.Show();
        
        _nextLevelButton.Initialize();
    }

    public override void Hide()
    {
        base.Hide();
        
        _nextLevelButton.Dispose();
    }
}
