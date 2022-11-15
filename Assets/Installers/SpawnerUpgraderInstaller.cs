using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerUpgraderInstaller : MonoInstaller
{
    [SerializeField] private SpawnerUpgrader _upgrader;

    public override void InstallBindings()
    {
        Container.Bind<SpawnerUpgrader>().FromInstance(_upgrader).AsSingle().NonLazy();
    }
}
