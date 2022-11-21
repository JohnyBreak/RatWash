using Zenject;
using UnityEngine;

public class BuyCanvasInstaller : MonoInstaller
{
    [SerializeField] private BuyCanvas _buyCanvas;

    public override void InstallBindings()
    {
        Container.Bind<BuyCanvas>().FromInstance(_buyCanvas).AsSingle().NonLazy();
    }
}
