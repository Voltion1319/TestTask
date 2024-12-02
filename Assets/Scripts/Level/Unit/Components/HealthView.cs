using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthView : MonoBehaviour, ITickable
{
    [SerializeField] private Image _fullHealthImage;
    [SerializeField] private Transform _mainTransform;

    private HealthComponent _healthComponent;
    private Camera _mainCamera;

    [Inject]
    public void Constructor(HealthComponent healthComponent, Camera mainCamera)
    {
        _healthComponent = healthComponent;
        _mainCamera = mainCamera;
    }

    public void Initialize()
    {
        SetHealth(_healthComponent.CurrentHealthPercent);
        
        _healthComponent.OnHealthPercentChanged += HealthComponentOnOnHealthPercentChanged;
    }

    public void Dispose()
    {
        _healthComponent.OnHealthPercentChanged -= HealthComponentOnOnHealthPercentChanged;
    }
    
    public void Tick()
    {
        _mainTransform.LookAt(_mainCamera.transform.position);
    }

    private void SetHealth(float currentHealth)
    {
        _fullHealthImage.fillAmount = currentHealth;
    }
    
    private void HealthComponentOnOnHealthPercentChanged(float currentHealth)
    {
        SetHealth(currentHealth);
    }
}
