using TMPro;
using UnityEngine;
using Zenject;

public class WalletCanvas : MonoBehaviour
{
    //[Inject] 
    private Wallet _wallet;
    [SerializeField] private string _moneyPrefix = "Money: ";
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void Awake()
    {
        _wallet.AmountChangeEvent += OnAmountChange;
    }

    private void OnDestroy()
    {
        _wallet.AmountChangeEvent -= OnAmountChange;
    }

    [Inject]
    private void Construct(Wallet wallet) 
    {
        _wallet = wallet;
    }

    private void OnAmountChange(int amount) 
    {
        _moneyText.text = _moneyPrefix + amount.ToString();
    }
}
