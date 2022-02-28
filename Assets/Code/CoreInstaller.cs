using UnityEngine;
using UserControlSystem.Models;
using Zenject;

public sealed class CoreInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
    }
}