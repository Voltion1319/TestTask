using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private string _buttonName = "Restart";
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    private LevelManager _levelManager;
    
    [Inject]
    public void Constructor(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public void Initialize()
    {
        _text.SetText(_buttonName);
        _button.onClick.AddListener(RestartButtonClick);
    }

    public void Dispose()
    {
        _button.onClick.RemoveListener(RestartButtonClick);
    }

    private void RestartButtonClick()
    {
        _levelManager.NextLevel();
    }
}
