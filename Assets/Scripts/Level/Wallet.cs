using System;

public class Wallet
{
    public event Action<int> OnWalletValueChanged;
    
    private int _walletValue;

    public int WalletValue
    {
        get => _walletValue;
        set
        {
            _walletValue = value;
            OnWalletValueChanged?.Invoke(_walletValue);
        }
    }

    public void Initialize()
    {
    }

    public void Dispose()
    {
    }

    public void AddGold(int goldCount)
    {
        WalletValue += goldCount;
    }
}
