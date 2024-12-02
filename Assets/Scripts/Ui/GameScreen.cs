using UnityEngine;

public class GameScreen : ScreenBase
{
    [SerializeField] private GoldViewer _goldViewer;

    public override void Show()
    {
        base.Show();
        
        _goldViewer.Initialize();
    }

    public override void Hide()
    {
        base.Hide();
        
        _goldViewer.Dispose();
    }
}
