using UnityEngine;
using Zenject;

public class WalletInstaller : MonoInstaller
{
    [SerializeField] private Wallet _wallet;

    public override void InstallBindings()
    {
        Container.Bind<Wallet>().FromInstance(_wallet).AsSingle().NonLazy();
    }
}