using UnityEngine;
using Zenject;
public class PreSpawnerInstaller : MonoInstaller
{
    [SerializeField] private PreSpawner _prespawner;

    public override void InstallBindings()
    {
        Container.Bind<PreSpawner>().FromInstance(_prespawner).AsSingle().NonLazy();
    }
}
