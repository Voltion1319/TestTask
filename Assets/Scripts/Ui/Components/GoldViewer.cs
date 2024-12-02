using TMPro;
using UnityEngine;
using Zenject;

public class GoldViewer : MonoBehaviour
{
    [SerializeField] private string _goldTextPatter = "{0}";
    [SerializeField] private TextMeshProUGUI _goldText;

    private Wallet _wallet;
    
    [Inject]
    public void Constructor(Wallet wallet)
    {
        _wallet = wallet;
    }
    
    public void Initialize()
    {
        SetText(_wallet.WalletValue);
        
        _wallet.OnWalletValueChanged += WalletOnOnWalletValueChanged;
    }

    public void Dispose()
    {
        _wallet.OnWalletValueChanged -= WalletOnOnWalletValueChanged;
    }

    private void SetText(int goldValue)
    {
        _goldText.SetText(string.Format(_goldTextPatter, goldValue));
    }
    
    private void WalletOnOnWalletValueChanged(int goldValue)
    {
        SetText(goldValue);
    }
}
