using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WasherUpgraderInstaller : MonoInstaller
{
    [SerializeField] private WasherUpgrader _upgrader;

    public override void InstallBindings()
    {
        Container.Bind<WasherUpgrader>().FromInstance(_upgrader).AsSingle().NonLazy();
    }
}
