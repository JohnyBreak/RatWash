using UnityEngine;
using Zenject;

public class GarbageInstaller : MonoInstaller
{
    [SerializeField] private Garbage _garbage;

    public override void InstallBindings()
    {
        Container.Bind<Garbage>().FromInstance(_garbage).AsSingle().NonLazy();
    }
}
