using UnityEngine;
using Zenject;

public class SaveManagerInstaller : MonoInstaller
{
    [SerializeField] private SaveManager _data;

    public override void InstallBindings()
    {
        Container.Bind<SaveManager>().FromInstance(_data).AsSingle().NonLazy();
    }
}
