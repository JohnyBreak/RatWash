using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartMoneyDropper : MonoBehaviour
{
    [SerializeField] private int _startAmount;
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void Awake()
    {
        _wallet.AddMoney(_startAmount);
    }
}
