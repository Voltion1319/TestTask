using System;

public class HealthComponent
{
    [Serializable]
    public class Settings
    {
        public int HealthCount = 10;
    }
    
    public event Action OnHealthEmpty;
    public event Action<int> OnHealthChanged;
    public event Action<float> OnHealthPercentChanged;
    
    private readonly Settings _settings;
    private int _currentHealth;
    private float _currentHealthPercent;

    public float CurrentHealthPercent => _currentHealthPercent;
    public bool IsAlive => CurrentHealth > 0;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (_currentHealth == value || !IsAlive)
            {
                return;
            }
            
            if (value < 0)
            {
                value = 0;
            }

            _currentHealth = value;
            _currentHealthPercent = (float)_currentHealth / _settings.HealthCount;
            OnHealthChanged?.Invoke(_currentHealth);
            OnHealthPercentChanged?.Invoke(_currentHealthPercent);

            if (!IsAlive)
            {
                OnHealthEmpty?.Invoke();
            }
        }
    }

    public HealthComponent(UnitSettings settings)
    {
        _settings = settings.HealthSettings;
    }

    public virtual void Initialize()
    {
        _currentHealth = _settings.HealthCount;
        _currentHealthPercent = 1;
    }

    public virtual void Dispose()
    {
        
    }

    public void GetDamage(DamageInfo damageInfo)
    {
        CurrentHealth -= damageInfo.Damage;
    }
}
