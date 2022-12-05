using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecyclingUpgraderInstaller : MonoInstaller
{
    [SerializeField] private RecyclingUpgrader _upgrader;

    public override void InstallBindings()
    {
        Container.Bind<RecyclingUpgrader>().FromInstance(_upgrader).AsSingle().NonLazy();
    }
}
