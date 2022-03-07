using Abstractions;
using Core;
using UnityEngine;
using UserControlSystem.Models;
using Zenject;

public sealed class CoreInstaller: MonoInstaller
{
    [SerializeField] private GameStatus _gameStatus;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TimeModel>().AsSingle();
        Container.Bind<IGameStatus>().FromInstance(_gameStatus);
    }
}