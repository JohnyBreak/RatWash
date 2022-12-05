using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecyclingInstaller : MonoInstaller
{
    [SerializeField] private RecyclingStorage _recycler;

    public override void InstallBindings()
    {
        Container.Bind<RecyclingStorage>().FromInstance(_recycler).AsSingle().NonLazy();
    }
}
