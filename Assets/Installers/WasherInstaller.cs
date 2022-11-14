using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WasherInstaller : MonoInstaller
{
    [SerializeField] private WasherStorage _washer;

    public override void InstallBindings()
    {
        Container.Bind<WasherStorage>().FromInstance(_washer).AsSingle().NonLazy();
    }
}
